USE FootballLeagueDB;
GO

-- 1. Ta bort en specifik matchstatistik (t.ex. vid felaktig inmatning)
DELETE FROM PlayerMatchStats
WHERE MatchID = 1 AND PlayerID = 9;

-- 2. Ta bort ett kontrakt (t.ex. om kontraktet rivs i förtid)
DELETE FROM Contracts
WHERE PlayerID = 13;

-- 3. Ta bort en arena som inte längre används
-- (Fungerar bara om inga lag eller matcher är kopplade till den)
DELETE FROM Arenas
WHERE ArenaID = 5 AND NOT EXISTS (SELECT 1 FROM Teams WHERE HomeArenaID = 5);