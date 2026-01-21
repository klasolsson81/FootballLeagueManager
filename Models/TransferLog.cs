namespace FootballLeagueManager.Models;

/// <summary>
/// Loggar historik över spelarövergångar.
/// </summary>
public class TransferLog
{
    public int LogId { get; set; } // Primärnyckel
    public int PlayerId { get; set; } // Vilken spelare det gäller
    public int? FromTeamId { get; set; } // Laget de lämnade
    public int ToTeamId { get; set; } // Laget de gick till
    public DateTime TransferDate { get; set; } // När övergången skedde

    // Navigationsegenskaper
    public Player Player { get; set; } = null!;
    public Team? FromTeam { get; set; }
    public Team ToTeam { get; set; } = null!;
}