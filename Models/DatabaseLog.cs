using System;

namespace FootballLeagueManager.Models;

/// <summary>
/// Representerar en rad i systemloggen (Audit Trail).
/// </summary>
public class DatabaseLog
{
    public int LogId { get; set; }
    public string TableName { get; set; } = null!;
    public string Operation { get; set; } = null!;
    public string LogMessage { get; set; } = null!;
    public string ChangedBy { get; set; } = null!;
    public DateTime ChangeDate { get; set; }
}