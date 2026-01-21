USE FootballLeagueDB;
GO

-- 1. Procedur för att hämta statistik för en specifik spelare
-- Använder inparametrar för att göra proceduren dynamisk[cite: 152, 158, 185, 203].
CREATE PROCEDURE sp_GetPlayerStats
    @PlayerID INT
AS
BEGIN
    SELECT 
        SUM(Goals) AS TotaltMål, 
        SUM(Assists) AS TotaltAssists, 
        SUM(YellowCards) AS TotaltGulaKort
    FROM PlayerMatchStats
    WHERE PlayerID = @PlayerID;
END;
GO

-- 2. Avancerad procedur: Flytta en spelare och logga händelsen
-- Använder en transaktion (Atomicitet) för att säkerställa att båda operationerna 
-- lyckas eller misslyckas tillsammans[cite: 232, 495, 498, 503, 513].
CREATE PROCEDURE sp_TransferPlayer
    @PlayerID INT,
    @FromTeamID INT,
    @ToTeamID INT
AS
BEGIN
    BEGIN TRY -- Felhantering [cite: 273, 274, 524]
        BEGIN TRANSACTION; -- Starta transaktion [cite: 503, 523]
        
        -- Uppdatera spelarens nuvarande lag
        UPDATE Players 
        SET TeamID = @ToTeamID 
        WHERE PlayerID = @PlayerID;

        -- Logga övergången i historiktabellen
        INSERT INTO TransferLogs (PlayerID, FromTeamID, ToTeamID, TransferDate)
        VALUES (@PlayerID, @FromTeamID, @ToTeamID, GETDATE());

        COMMIT TRANSACTION; -- Bekräfta ändringar [cite: 513, 536]
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; -- Återställ vid fel [cite: 515, 540]
        PRINT ERROR_MESSAGE(); -- Visa felmeddelande [cite: 278, 541]
    END CATCH
END;
GO