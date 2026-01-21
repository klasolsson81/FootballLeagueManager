USE FootballLeagueDB;
GO

-- 1. Vy för att se komplett spelarinfo (Förenkling)
-- Abstraherar bort JOIN-logiken för slutanvändaren[cite: 554, 555].
CREATE VIEW vw_PlayerDetails AS
SELECT 
    p.FirstName + ' ' + p.LastName AS FullName, 
    pos.Title AS Position, 
    t.TeamName AS Lag
FROM Players p
JOIN Positions pos ON p.PositionID = pos.PositionID
JOIN Teams t ON p.TeamID = t.TeamID;
GO

-- 2. Vy för Säkerhet: Visa kontraktslängd utan att avslöja lön
-- Döljer känsliga kolumner (Salary) för obehöriga[cite: 550, 571, 572, 579, 580].
CREATE VIEW vw_PublicContractInfo AS
SELECT 
    PlayerID, 
    EndDate
FROM Contracts;
GO