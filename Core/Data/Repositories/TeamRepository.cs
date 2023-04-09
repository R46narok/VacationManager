using Core.Data.Entities;
using Core.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly VacationManagerDbContext _context;

    public TeamRepository(VacationManagerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<Team> GetTeams()
    {
        return _context.Teams.Include(x => x.TeamLeader).AsQueryable();
    }

    public Team GetTeam(string id)
    {
        return _context.Teams.Find(id);
    }

    public void AddTeam(Team team)
    {
        _context.Teams.Add(team);
        _context.SaveChanges();
    }

    public void EditTeam(Team team)
    {
        _context.Teams.Update(team);
        _context.SaveChanges();
    }

    public void DeleteTeam(Team team)
    {
        _context.Teams.Remove(team);
        _context.SaveChanges();
    }
}