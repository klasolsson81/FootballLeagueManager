using System.Diagnostics.Contracts;

namespace FootballLeagueManager.Models;

/// <summary>
/// Representerar en spelare i ligan.
/// </summary>
public class Player
{
    public int PlayerId { get; set; } // Primärnyckel
    public string FirstName { get; set; } = null!; // Förnamn
    public string LastName { get; set; } = null!; // Efternamn
    public DateTime DateOfBirth { get; set; } // Födelsedatum
    public int TeamId { get; set; } // FK till Teams
    public int PositionId { get; set; } // FK till Positions

    // Navigationsegenskaper för JOINS
    public Team Team { get; set; } = null!;
    public Position Position { get; set; } = null!;
    public Contract? Contract { get; set; }
    public ICollection<PlayerMatchStat> MatchStats { get; set; } = new List<PlayerMatchStat>();
}