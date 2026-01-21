namespace FootballLeagueManager.Models;

/// <summary>
/// Kopplingstabell för statistik per spelare och match (N:M).
/// </summary>
public class PlayerMatchStat
{
    public int MatchId { get; set; } // Del av sammansatt PK
    public int PlayerId { get; set; } // Del av sammansatt PK
    public int Goals { get; set; } // Antal mål
    public int Assists { get; set; } // Antal assist
    public int YellowCards { get; set; } // Gula kort

    // Navigationsegenskaper
    public Match Match { get; set; } = null!;
    public Player Player { get; set; } = null!;
}