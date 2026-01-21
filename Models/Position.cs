using System.Numerics;

namespace FootballLeagueManager.Models;

/// <summary>
/// Representerar en spelares position (t.ex. Målvakt, Back).
/// </summary>
public class Position
{
    public int PositionId { get; set; } // Primärnyckel
    public string Title { get; set; } = null!; // Namn på positionen

    // Navigationsegenskap: En position kan hållas av många spelare
    public ICollection<Player> Players { get; set; } = new List<Player>();
}