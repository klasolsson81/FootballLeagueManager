USE FootballLeagueDB;
GO

-- 1. Skapa logg-tabellen om den inte redan finns
-- Vi kontrollerar existens för att undvika felet "object named DatabaseLogs already exists"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DatabaseLogs]') AND type in (N'U'))
BEGIN
    CREATE TABLE DatabaseLogs (
        LogID INT PRIMARY KEY IDENTITY(1,1),
        TableName VARCHAR(50),
        Operation VARCHAR(20),
        LogMessage VARCHAR(MAX),
        ChangedBy VARCHAR(100) DEFAULT USER_NAME(),
        ChangeDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 2. Ta bort triggern om den redan finns för att kunna skapa den på nytt
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_LogSalaryUpdate')
BEGIN
    DROP TRIGGER trg_LogSalaryUpdate;
END
GO

-- 3. Skapa triggern för loggning av löneändringar
-- Vi hämtar spelarens namn via JOIN för bättre UX i loggen
CREATE TRIGGER trg_LogSalaryUpdate
ON Contracts
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DatabaseLogs (TableName, Operation, LogMessage)
    SELECT 
        'Contracts', 
        'UPDATE', 
        'Lön ändrad för ' + p.FirstName + ' ' + p.LastName + 
        '. Gammal: ' + FORMAT(d.Salary, 'C', 'sv-SE') + 
        ' -> Ny: ' + FORMAT(i.Salary, 'C', 'sv-SE')
    FROM inserted i
    JOIN deleted d ON i.ContractId = d.ContractId
    JOIN Players p ON i.PlayerId = p.PlayerId; -- Kopplar till Players för att få namnet
END;
GO