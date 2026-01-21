namespace FootballLeagueManager.Models;

/// <summary>
/// Representerar en spelares kontrakt och lön.
/// </summary>
public class Contract
{
    public int ContractId { get; set; } // Primärnyckel
    public int PlayerId { get; set; } // Unik FK till Player (1:1)
    public decimal Salary { get; set; } // Månadslön
    public DateTime EndDate { get; set; } // Kontraktets slutdatum

    // Navigationsegenskap
    public Player Player { get; set; } = null!;
}