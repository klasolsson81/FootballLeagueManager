using System.Numerics;

namespace FootballLeagueManager.Models;

/// <summary>
/// Representerar ett fotbollslag.
/// </summary>
public class Team
{
    public int TeamId { get; set; } // Primärnyckel
    public string TeamName { get; set; } = null!; // Lagets namn
    public string City { get; set; } = null!; // Stad
    public int? HomeArenaId { get; set; } // Främmande nyckel till Arena

    // Navigationsegenskaper
    public Arena? HomeArena { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
}