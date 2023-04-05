using System.Linq;
using Models;

namespace Repositories.Interfaces;

public interface ITeamRepository
{
    IQueryable<Team> GetTeams();
    Team GetTeam(string id);
    void AddTeam(Team team);
    void EditTeam(Team team);
    void DeleteTeam(Team team);
}