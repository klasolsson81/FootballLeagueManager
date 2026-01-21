USE FootballLeagueDB;
GO

-- 1. Uppdatera lönen för en specifik spelare (Löneförhöjning för Ramon Pascal Lundqvist)
UPDATE Contracts
SET Salary = 155000.00
WHERE PlayerID = 10;

-- 2. Ändra kapaciteten på en arena (Ombyggnad av Gamla Ullevi)
UPDATE Arenas
SET Capacity = 19000
WHERE ArenaID = 1;

-- 3. Flytta en spelare till ett annat lag (Demonstrerar FK-uppdatering)
UPDATE Players
SET TeamID = 3 -- Flyttar Linus Carlstrand till BK Häcken
WHERE PlayerID = 13;