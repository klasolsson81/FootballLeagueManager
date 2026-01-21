using Microsoft.EntityFrameworkCore;
using FootballLeagueManager.Data;
using FootballLeagueManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace FootballLeagueManager.Services;

public class PlayerRepository
{
    private readonly AppDbContext _context;

    public PlayerRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Player> GetFullPlayerReport()
    {
        // Inkluderar all data för Master-rapporten
        return _context.Players
            .Include(p => p.Position)
            .Include(p => p.Team)
            .Include(p => p.Contract)
            .Include(p => p.MatchStats)
            .Include(p => p.TransferLogs)
            .OrderBy(p => p.LastName)
            .ToList();
    }

    public List<Player> GetPlayersByTeam(int teamId)
    {
        // Sortering per position för att undvika "huller om buller"
        return _context.Players
            .Where(p => p.TeamId == teamId)
            .Include(p => p.Position)
            .Include(p => p.Team)
            .OrderBy(p => p.PositionId)
            .ThenBy(p => p.LastName)
            .ToList();
    }

    public List<Player> GetAllPlayers() =>
        _context.Players.Include(p => p.Team).Include(p => p.Position).OrderBy(p => p.PlayerId).ToList();

    public Player? GetPlayerWithDetails(int id) =>
        _context.Players.Include(p => p.Team).ThenInclude(t => t.HomeArena)
        .Include(p => p.Position).Include(p => p.Contract).Include(p => p.MatchStats).FirstOrDefault(p => p.PlayerId == id);

    public bool UpdatePlayerSalary(int id, decimal salary)
    {
        var c = _context.Contracts.FirstOrDefault(x => x.PlayerId == id);
        if (c == null) return false;
        c.Salary = salary;
        _context.SaveChanges(); // Sparar ändringen i SQL [cite: 145]
        return true;
    }
}