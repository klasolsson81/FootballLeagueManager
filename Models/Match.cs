namespace FootballLeagueManager.Models;

/// <summary>
/// Representerar en spelad eller kommande match.
/// </summary>
public class Match
{
    public int MatchId { get; set; } // Primärnyckel
    public DateTime MatchDate { get; set; } // Datum och tid
    public int HomeTeamId { get; set; } // FK till hemma-laget
    public int AwayTeamId { get; set; } // FK till borta-laget
    public int ArenaId { get; set; } // FK till var matchen spelas

    // Navigationsegenskaper
    public Team HomeTeam { get; set; } = null!;
    public Team AwayTeam { get; set; } = null!;
    public Arena Arena { get; set; } = null!;
    public ICollection<PlayerMatchStat> PlayerStats { get; set; } = new List<PlayerMatchStat>();
}