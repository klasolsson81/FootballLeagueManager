using Spectre.Console;
using FootballLeagueManager.Models;
using FootballLeagueManager.Services;
using FootballLeagueManager.Data;

namespace FootballLeagueManager.UI;

/// <summary>
/// Hanterar applikationens användargränssnitt och presentation (SRP).
/// Använder Spectre.Console för professionell visualisering.
/// </summary>
public class MainMenu
{
    private readonly PlayerRepository _playerRepo;

    public MainMenu(PlayerRepository playerRepo)
    {
        _playerRepo = playerRepo;
    }

    /// <summary>
    /// Startar huvudloopen för programmet.
    /// </summary>
    public void Run()
    {
        bool isRunning = true;
        while (isRunning)
        {
            try
            {
                AnsiConsole.Clear();
                // Skapar en visuell rubrik för applikationen
                AnsiConsole.Write(new FigletText("IFK MANAGER 2026").Centered().Color(Color.Blue));

                // Interaktiv huvudmeny
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]VÄLJ HANDLING I MENYN:[/]")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Visa Trupp (Sorterad)",
                            "Fullständig Spelarrapport (Master)",
                            "Visa Transferlogg (Historik)",
                            "Spelardetaljer & Matchstatistik",
                            "Uppdatera Lön (Löneförhandling)",
                            "Visa Systemlogg (Audit Trail)",
                            "Avsluta"
                        }));

                if (choice == "Avsluta") break;

                switch (choice)
                {
                    case "Visa Trupp (Sorterad)": ShowFilteredList(); break;
                    case "Fullständig Spelarrapport (Master)": ShowMasterTable(); break;
                    case "Visa Transferlogg (Historik)": ShowTransferHistory(); break;
                    case "Spelardetaljer & Matchstatistik": ShowPlayerDetails(); break;
                    case "Uppdatera Lön (Löneförhandling)": UpdateSalaryUX(); break;
                    case "Visa Systemlogg (Audit Trail)": ShowSystemLogs(); break;
                }
            }
            catch (Exception ex)
            {
                // Fångar upp oväntade fel för att förhindra krasch (VG-krav)
                AnsiConsole.MarkupLine($"[bold red]ETT FEL UPPSTOD:[/] {ex.Message}");
                PressAnyKey();
            }
        }
    }

    // --- 1. LISTA TRUPP ---
    private void ShowFilteredList()
    {
        var id = AnsiConsole.Ask<int>("[white]Ange Lag-ID att visa (1 = IFK Göteborg):[/]");
        var players = _playerRepo.GetPlayersByTeam(id);

        var table = new Table().Border(TableBorder.Rounded).Title($"[blue]TRUPPLISTA LAG {id}[/]");
        table.AddColumn("Namn");
        table.AddColumn("Position");

        foreach (var p in players)
        {
            table.AddRow($"{p.FirstName} {p.LastName}", p.Position.Title);
        }

        AnsiConsole.Write(table);
        PressAnyKey();
    }

    // --- 2. MASTER-RAPPORT ---
    private void ShowMasterTable()
    {
        var report = _playerRepo.GetFullPlayerReport();
        var table = new Table().Border(TableBorder.Rounded).Expand();
        table.Title = new TableTitle("[bold yellow]MASTER-RAPPORT: ALLA RELATIONER & STATS[/]");

        table.AddColumn("Spelare");
        table.AddColumn("Pos");
        table.AddColumn("Månadslön");
        table.AddColumn("Kontrakt Slut");
        table.AddColumn("Mål/Assists");
        table.AddColumn("Senaste Transfer");

        foreach (var p in report)
        {
            // Aggregerar matchstatistik direkt från kopplingstabellen
            int goals = p.MatchStats?.Sum(s => s.Goals) ?? 0;
            int assists = p.MatchStats?.Sum(s => s.Assists) ?? 0;

            // Hämtar senaste loggade händelse
            string transfer = p.TransferLogs?.OrderByDescending(l => l.TransferDate).FirstOrDefault()?.TransferDate.ToString("yyyy-MM-dd") ?? "Ingen logg";

            table.AddRow(
                $"{p.FirstName} {p.LastName}",
                p.Position.Title.Substring(0, 2),
                p.Contract?.Salary.ToString("C0") ?? "N/A",
                p.Contract?.EndDate.ToString("yyyy-MM") ?? "N/A",
                $"{goals}/{assists}",
                transfer
            );
        }
        AnsiConsole.Write(table);
        PressAnyKey();
    }

    // --- 3. TRANSFERLOGG ---
    private void ShowTransferHistory()
    {
        var report = _playerRepo.GetFullPlayerReport();
        var table = new Table().Border(TableBorder.Double).Title("[bold magenta]TRANSFERHISTORIK - ALLA ÖVERGÅNGAR[/]");

        table.AddColumn("Datum");
        table.AddColumn("Spelare");
        table.AddColumn("Från Lag-ID");
        table.AddColumn("Nuvarande Lag");

        foreach (var p in report)
        {
            foreach (var log in p.TransferLogs)
            {
                table.AddRow(
                    log.TransferDate.ToString("yyyy-MM-dd"),
                    $"{p.FirstName} {p.LastName}",
                    log.FromTeamId?.ToString() ?? "Initial trupp",
                    p.Team.TeamName
                );
            }
        }
        AnsiConsole.Write(table);
        PressAnyKey();
    }

    // --- 4. SPELARDETALJER ---
    private void ShowPlayerDetails()
    {
        var players = _playerRepo.GetAllPlayers();
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<Player>()
                .Title("Välj spelare för djupanalys:")
                .UseConverter(p => $"{p.FirstName} {p.LastName}")
                .AddChoices(players));

        var p = _playerRepo.GetPlayerWithDetails(selected.PlayerId);

        var grid = new Grid().AddColumn().AddColumn();
        grid.AddRow("[yellow]Position:[/] ", p.Position.Title);
        grid.AddRow("[yellow]Hemarena:[/] ", p.Team.HomeArena?.Name ?? "N/A");
        grid.AddRow("[green]Matchstatistik:[/] ", $"{p.MatchStats.Sum(s => s.Goals)} Mål / {p.MatchStats.Sum(s => s.Assists)} Assists");

        AnsiConsole.Write(new Panel(grid).Header($"{p.FirstName.ToUpper()} {p.LastName.ToUpper()}").BorderColor(Color.Blue));
        PressAnyKey();
    }

    // --- 5. LÖNEFÖRHANDLING ---
    private void UpdateSalaryUX()
    {
        var players = _playerRepo.GetAllPlayers();
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<Player>()
                .Title("Välj spelare för löneförhandling:")
                .UseConverter(p => $"{p.FirstName} {p.LastName}")
                .AddChoices(players));

        var p = _playerRepo.GetPlayerWithDetails(selected.PlayerId);

        // UX: Visa nuvarande värden innan ändring
        AnsiConsole.Write(new Panel($"[white]Nuvarande lön:[/] [green]{p.Contract?.Salary:C}[/]")
            .Header("AKTUELLT KONTRAKT")
            .BorderColor(Color.Yellow));

        if (!AnsiConsole.Confirm("Vill du påbörja löneförhandlingen?")) return;

        var newSalary = AnsiConsole.Ask<decimal>("Ange den nya månadslönen:");

        if (_playerRepo.UpdatePlayerSalary(p.PlayerId, newSalary))
            AnsiConsole.MarkupLine("[bold green]KLART![/] Databasen har uppdaterats och ändringen har loggats.");
        else
            AnsiConsole.MarkupLine("[red]Kunde inte hitta ett giltigt kontrakt för denna spelare.[/]");

        PressAnyKey();
    }

    // --- 6. SYSTEMLOGG ---
    private void ShowSystemLogs()
    {
        using var db = new AppDbContext();
        // Hämtar de 15 senaste loggarna
        var logs = db.DatabaseLogs.OrderByDescending(l => l.ChangeDate).Take(15).ToList();

        var table = new Table().Border(TableBorder.Rounded).Title("[red]SYSTEMLOGG - AUDIT TRAIL[/]");
        table.AddColumn("Datum & Tid");
        table.AddColumn("Operation");
        table.AddColumn("Beskrivning");

        foreach (var l in logs)
        {
            // Ändrat från "HH:mm:ss" till fullständigt datum och tid
            table.AddRow(
                l.ChangeDate.ToString("yyyy-MM-dd HH:mm:ss"),
                $"[white]{l.Operation}[/]",
                l.LogMessage
            );
        }

        AnsiConsole.Write(table);
        PressAnyKey();
    }

    // --- HJÄLPMETODER ---
    private void PressAnyKey()
    {
        AnsiConsole.MarkupLine("\n[grey]Tryck på valfri tangent för att återgå till huvudmenyn...[/]");
        Console.ReadKey(true);
    }
}