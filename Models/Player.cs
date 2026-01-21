using System.Collections.Generic;

namespace FootballLeagueManager.Models;

public class Player
{
    public int PlayerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public int TeamId { get; set; }
    public int PositionId { get; set; }

    public Team Team { get; set; } = null!;
    public Position Position { get; set; } = null!;
    public Contract? Contract { get; set; }
    public ICollection<PlayerMatchStat> MatchStats { get; set; } = new List<PlayerMatchStat>();
    public ICollection<TransferLog> TransferLogs { get; set; } = new List<TransferLog>();
}