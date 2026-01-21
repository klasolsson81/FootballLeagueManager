using System.Text.RegularExpressions;

namespace FootballLeagueManager.Models;

/// <summary>
/// Representerar en fotbollsarena.
/// </summary>
public class Arena
{
    public int ArenaId { get; set; } // Primärnyckel
    public string Name { get; set; } = null!; // Arenans namn
    public int Capacity { get; set; } // Publikkapacitet
    public string? PitchType { get; set; } // Underlag (t.ex. Gräs)

    // Navigationsegenskaper
    public ICollection<Team> Teams { get; set; } = new List<Team>();
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}