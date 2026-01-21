USE FootballLeagueDB;
GO

-- 1. Hämta alla spelare i ligan
SELECT * FROM Players;

-- 2. Hämta alla lag och deras hemstads-info
SELECT TeamName, City FROM Teams;

-- 3. Hämta alla arenor med en kapacitet över 15 000
SELECT Name, Capacity FROM Arenas 
WHERE Capacity > 15000;

-- 4. Hämta alla kontrakt som går ut under 2026
SELECT * FROM Contracts 
WHERE EndDate BETWEEN '2026-01-01' AND '2026-12-31';