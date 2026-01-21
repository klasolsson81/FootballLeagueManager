using Microsoft.EntityFrameworkCore;
using FootballLeagueManager.Models;

namespace FootballLeagueManager.Data;

public class AppDbContext : DbContext
{
    // Befintliga tabeller
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Arena> Arenas { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    public DbSet<PlayerMatchStat> PlayerMatchStats { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<TransferLog> TransferLogs { get; set; } = null!;
    public DbSet<DatabaseLog> DatabaseLogs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Anslutningssträng baserad på din server PAPPAS\SQLEXPRESS
        optionsBuilder.UseSqlServer(@"Server=PAPPAS\SQLEXPRESS;Database=FootballLeagueDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mappar primärnycklar som inte heter "Id"
        modelBuilder.Entity<Position>().HasKey(p => p.PositionId);
        modelBuilder.Entity<Arena>().HasKey(a => a.ArenaId);
        modelBuilder.Entity<Team>().HasKey(t => t.TeamId);
        modelBuilder.Entity<Player>().HasKey(p => p.PlayerId);
        modelBuilder.Entity<Match>().HasKey(m => m.MatchId);
        modelBuilder.Entity<Contract>().HasKey(c => c.ContractId);
        modelBuilder.Entity<TransferLog>().HasKey(tl => tl.LogId);

        // Mappar nyckel för logg-tabellen
        modelBuilder.Entity<DatabaseLog>().HasKey(dl => dl.LogId);

        modelBuilder.Entity<PlayerMatchStat>()
            .HasKey(pms => new { pms.MatchId, pms.PlayerId });

        base.OnModelCreating(modelBuilder);
    }
}