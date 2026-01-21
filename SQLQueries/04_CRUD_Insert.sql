USE FootballLeagueDB;
GO

-- 1. Lägg till en ny arena
INSERT INTO Arenas (Name, Capacity, PitchType) 
VALUES ('Borås Arena', 16200, 'Konstgräs');

-- 2. Lägg till ett nytt lag (Elfsborg) kopplat till den nya arenan
INSERT INTO Teams (TeamName, City, HomeArenaID) 
VALUES ('IF Elfsborg', 'Borås', 7);

-- 3. Lägg till en ny spelare i IFK Göteborg (för att visa 1:N)
INSERT INTO Players (FirstName, LastName, DateOfBirth, TeamID, PositionID) 
VALUES ('Sebastian', 'Ohlsén', '2004-05-10', 1, 3);

-- 4. Skapa ett kontrakt för den nya spelaren
INSERT INTO Contracts (PlayerID, Salary, EndDate) 
VALUES (14, 35000.00, '2026-12-31');