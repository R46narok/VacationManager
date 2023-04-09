using Core.Data.Entities;

namespace Core.Data.Repositories.Interfaces;

public interface ITeamRepository
{
    IQueryable<Team> GetTeams();
    Team GetTeam(string id);
    void AddTeam(Team team);
    void EditTeam(Team team);
    void DeleteTeam(Team team);
}