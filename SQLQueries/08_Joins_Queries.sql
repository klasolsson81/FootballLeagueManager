USE FootballLeagueDB;
GO

-- 1. TRUPPLISTA MED POSITIONER (JOIN)
-- Kopplar ihop Players med Positions för att se titeln istället för ett ID.
SELECT 
    p.FirstName + ' ' + p.LastName AS Spelare,
    pos.Title AS Position,
    p.DateOfBirth AS Födelsedatum
FROM Players p
JOIN Positions pos ON p.PositionID = pos.PositionID
ORDER BY pos.Title;

-- 2. SKYTTELIGAN (JOIN + GROUP BY + SUM)
-- Kopplar Players till PlayerMatchStats för att räkna ut totala mål.
-- Detta visar att din Många-till-Många-relation fungerar.
SELECT 
    p.FirstName + ' ' + p.LastName AS Spelare,
    SUM(pms.Goals) AS TotalaMål,
    SUM(pms.Assists) AS TotalaAssists
FROM Players p
JOIN PlayerMatchStats pms ON p.PlayerID = pms.PlayerID
GROUP BY p.FirstName, p.LastName
HAVING SUM(pms.Goals) > 0
ORDER BY TotalaMål DESC;

-- 3. EKONOMISK ÖVERBLICK (JOIN + AVG/SUM)
-- Visar lön per position. Viktigt för att visa hantering av DECIMAL-data.
SELECT 
    pos.Title AS Position,
    COUNT(p.PlayerID) AS AntalSpelare,
    CAST(AVG(c.Salary) AS DECIMAL(18, 2)) AS Medellön,
    SUM(c.Salary) AS TotalLönekostnad
FROM Players p
JOIN Positions pos ON p.PositionID = pos.PositionID
JOIN Contracts c ON p.PlayerID = c.PlayerID
GROUP BY pos.Title;

-- 4. MATCHHISTORIK (MULTIPLE JOINS)
-- Hämtar matchinfo och ersätter ID:n med riktiga lagnamn och arenanamn.
SELECT 
    m.MatchDate AS Datum,
    t1.TeamName AS Hemmalag,
    t2.TeamName AS Bortalag,
    a.Name AS Arena
FROM Matches m
JOIN Teams t1 ON m.HomeTeamID = t1.TeamID
JOIN Teams t2 ON m.AwayTeamID = t2.TeamID
JOIN Arenas a ON m.ArenaID = a.ArenaID
ORDER BY m.MatchDate DESC;

-- 5. SPELARE UTAN KONTRAKT (LEFT JOIN)
-- Visar spelare som finns i systemet men saknar inlagda kontrakt.
-- Bra för att visa att du förstår skillnaden på JOIN och LEFT JOIN.
SELECT 
    p.FirstName, 
    p.LastName
FROM Players p
LEFT JOIN Contracts c ON p.PlayerID = c.PlayerID
WHERE c.ContractID IS NULL;