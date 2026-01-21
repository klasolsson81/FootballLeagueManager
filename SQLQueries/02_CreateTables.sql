USE FootballLeagueDB;
GO

CREATE TABLE Positions (
    PositionID INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Arenas (
    ArenaID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL UNIQUE,
    Capacity INT CHECK (Capacity > 0),
    PitchType VARCHAR(50) DEFAULT 'Gräs'
);

CREATE TABLE Teams (
    TeamID INT PRIMARY KEY IDENTITY(1,1),
    TeamName VARCHAR(100) NOT NULL UNIQUE,
    City VARCHAR(100) NOT NULL,
    HomeArenaID INT,
    FOREIGN KEY (HomeArenaID) REFERENCES Arenas(ArenaID)
);

CREATE TABLE Players (
    PlayerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    TeamID INT NOT NULL,
    PositionID INT NOT NULL,
    FOREIGN KEY (TeamID) REFERENCES Teams(TeamID),
    FOREIGN KEY (PositionID) REFERENCES Positions(PositionID)
);

CREATE TABLE Matches (
    MatchID INT PRIMARY KEY IDENTITY(1,1),
    MatchDate DATETIME NOT NULL,
    HomeTeamID INT NOT NULL,
    AwayTeamID INT NOT NULL,
    ArenaID INT NOT NULL,
    FOREIGN KEY (HomeTeamID) REFERENCES Teams(TeamID),
    FOREIGN KEY (AwayTeamID) REFERENCES Teams(TeamID),
    FOREIGN KEY (ArenaID) REFERENCES Arenas(ArenaID),
    CONSTRAINT CK_Matches_NoSelfPlay CHECK (HomeTeamID <> AwayTeamID)
);

-- Kopplingstabell för Många-till-Många
CREATE TABLE PlayerMatchStats (
    MatchID INT NOT NULL,
    PlayerID INT NOT NULL,
    Goals INT DEFAULT 0 CHECK (Goals >= 0),
    Assists INT DEFAULT 0,
    YellowCards INT DEFAULT 0,
    PRIMARY KEY (MatchID, PlayerID),
    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID),
    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID)
);

CREATE TABLE Contracts (
    ContractID INT PRIMARY KEY IDENTITY(1,1),
    PlayerID INT NOT NULL UNIQUE,
    Salary DECIMAL(18, 2) NOT NULL CHECK (Salary > 0),
    EndDate DATE NOT NULL,
    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID)
);

CREATE TABLE TransferLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    PlayerID INT NOT NULL,
    FromTeamID INT,
    ToTeamID INT NOT NULL,
    TransferDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID)
);