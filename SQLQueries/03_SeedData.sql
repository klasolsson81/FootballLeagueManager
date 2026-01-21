USE FootballLeagueDB;
GO

-- =============================================
-- 1. POSITIONS (Stödjer 1:N till Players)
-- =============================================
INSERT INTO Positions (Title) VALUES ('Målvakt'), ('Back'), ('Mittfältare'), ('Anfallare');

-- =============================================
-- 2. ARENAS
-- =============================================
INSERT INTO Arenas (Name, Capacity, PitchType) VALUES 
('Gamla Ullevi', 18416, 'Hybridgräs'),
('Eleda Stadion', 22500, 'Gräs'),
('Bravida Arena', 6300, 'Konstgräs'),
('Friends Arena', 50000, 'Gräs'),
('Örjans Vall', 10873, 'Gräs'),
('Tele2 Arena', 30000, 'Konstgräs');

-- =============================================
-- 3. TEAMS
-- =============================================
INSERT INTO Teams (TeamName, City, HomeArenaID) VALUES 
('IFK Göteborg', 'Göteborg', 1),
('Malmö FF', 'Malmö', 2),
('BK Häcken', 'Göteborg', 3),
('AIK', 'Solna', 4),
('Halmstads BK', 'Halmstad', 5),
('Djurgårdens IF', 'Stockholm', 6);

-- =============================================
-- 4. PLAYERS (IFK Göteborgs trupp 2026)
-- =============================================
-- PositionID: 1=MV, 2=Back, 3=MF, 4=Anfall
INSERT INTO Players (FirstName, LastName, DateOfBirth, TeamID, PositionID) VALUES 
('Elis', 'Bishesari', '2005-05-19', 1, 1),
('Pontus', 'Dahlberg', '1999-01-21', 1, 1),
('Jonas', 'Bager', '1996-07-18', 1, 2),
('August', 'Erlingmark', '1998-04-22', 1, 2),
('Noah', 'Tolf', '2005-06-28', 1, 2),
('Thomas', 'Santos', '1998-10-10', 1, 2),
('Kolbeinn', 'Thordarson', '2000-03-12', 1, 3),
('Filip', 'Ottosson', '1996-09-12', 1, 3),
('Tobias', 'Heintz', '1998-07-13', 1, 3),
('Ramon Pascal', 'Lundqvist', '1997-05-10', 1, 3),
('Arbnor', 'Mucolli', '1999-09-15', 1, 3),
('Max', 'Fenger', '2001-08-04', 1, 4),
('Linus', 'Carlstrand', '2004-08-31', 1, 4);

-- =============================================
-- 5. CONTRACTS (Med realistiska löner & datum)
-- =============================================
INSERT INTO Contracts (PlayerID, Salary, EndDate) VALUES 
(1, 45000.00, '2027-12-31'),
(2, 95000.00, '2026-12-31'),
(3, 110000.00, '2027-12-31'),
(4, 105000.00, '2028-12-31'),
(5, 40000.00, '2029-06-30'),
(6, 115000.00, '2026-06-30'),
(7, 100000.00, '2028-12-31'),
(8, 90000.00, '2029-12-31'),
(9, 135000.00, '2027-12-31'),
(10, 140000.00, '2027-12-31'),
(11, 125000.00, '2026-12-31'),
(12, 90000.00, '2027-12-31'),
(13, 55000.00, '2026-12-31');

-- =============================================
-- 6. MATCHES (Från din undersökning)
-- =============================================
INSERT INTO Matches (MatchDate, HomeTeamID, AwayTeamID, ArenaID) VALUES 
('2025-11-09 15:00:00', 1, 4, 1), -- IFK Gbg vs AIK (Exempel för statistik)
('2025-11-02 15:00:00', 6, 1, 6), -- Djurgården vs IFK Gbg
('2025-10-26 15:00:00', 5, 1, 5); -- Halmstad vs IFK Gbg

-- =============================================
-- 7. PLAYERMATCHSTATS (N:M relation)
-- =============================================
-- Match 1 (v AIK): Heintz & Mucolli mål
INSERT INTO PlayerMatchStats (MatchID, PlayerID, Goals, Assists, YellowCards) VALUES 
(1, 9, 1, 0, 0), -- Heintz
(1, 11, 1, 0, 0); -- Mucolli

-- Match 2 (v Djurgården): Gula kort Bager & Thordarson
INSERT INTO PlayerMatchStats (MatchID, PlayerID, Goals, Assists, YellowCards) VALUES 
(2, 3, 0, 0, 1), -- Bager
(2, 7, 0, 0, 1); -- Thordarson

-- Match 3 (v Halmstad): Santos mål, Ramon 2 mål
INSERT INTO PlayerMatchStats (MatchID, PlayerID, Goals, Assists, YellowCards) VALUES 
(3, 6, 1, 0, 0), -- Santos
(3, 10, 2, 0, 0); -- Ramon

-- =============================================
-- 8. TRANSFERLOGS (Historik)
-- =============================================
INSERT INTO TransferLogs (PlayerID, FromTeamID, ToTeamID, TransferDate) VALUES 
(3, NULL, 1, '2024-07-16'), -- Jonas Bager från Charleroi
(6, NULL, 1, '2023-07-01'), -- Thomas Santos från AC Horsens
(10, NULL, 1, '2024-01-01'); -- Ramon Pascal Lundqvist från Göztepe