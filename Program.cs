using FootballLeagueManager.Data;
using FootballLeagueManager.Services;
using FootballLeagueManager.UI;

// Skapa resurser (IDisposable hanteras med using)
using var context = new AppDbContext();

// Initiera arkitekturen (SRP)
var playerRepo = new PlayerRepository(context);
var menu = new MainMenu(playerRepo);

// Kör applikationen
menu.Run();