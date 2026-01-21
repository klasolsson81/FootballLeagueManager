using FootballLeagueManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueManager.Data;

/// <summary>
/// Ansvarar för kopplingen till databasen och mappar klasser till tabeller (SRP).
/// </summary>
public class AppDbContext : DbContext
{
    // Tabeller i databasen representerade som DbSet
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Arena> Arenas { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    public DbSet<PlayerMatchStat> PlayerMatchStats { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<TransferLog> TransferLogs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Anslutningssträng baserad på din bild (PAPPAS\SQLEXPRESS)
        // TrustServerCertificate=True krävs för moderna .NET-versioner mot lokal SQL
        optionsBuilder.UseSqlServer(@"Server=PAPPAS\SQLEXPRESS;Database=FootballLeagueDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfigurera sammansatt primärnyckel för kopplingstabellen (Många-till-Många)
        modelBuilder.Entity<PlayerMatchStat>()
            .HasKey(pms => new { pms.MatchId, pms.PlayerId });

        // Här kan vi även lägga till ytterligare konfigurationer om det behövs senare
        base.OnModelCreating(modelBuilder);
    }
}