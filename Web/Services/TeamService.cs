using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;

namespace Web.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    }

    public List<Team> GetTeams()
    {
        return _teamRepository.GetTeams().ToList();
    }

    public Team GetTeam(string id)
    {
        return _teamRepository.GetTeam(id);
    }

    public void AddTeam(Team team)
    {
        _teamRepository.AddTeam(team);
    }

    public void AddTeamLead(User user, Team team)
    {
        team.TeamLeader = user;
        _teamRepository.EditTeam(team);
    }

    public void RemoveTeamLead(Team team)
    {
        team.TeamLeader = null;
        _teamRepository.EditTeam(team);
    }

    public void AddUserToTeam(User user, Team team)
    {
        team.Developers.Add(user);
        _teamRepository.EditTeam(team);
    }

    public void RemoveUserFromTeam(User user, Team team)
    {
        team.Developers.Remove(user);
        _teamRepository.EditTeam(team);
    }

    public void AddProjectToTeam(Project project, Team team)
    {
        team.Project = project;
        _teamRepository.EditTeam(team);
    }

    public void RemoveProjectFromTeam(Team team)
    {
        team.Project = null;
        _teamRepository.EditTeam(team);
    }

    public void EditTeam(Team team)
    {
        _teamRepository.EditTeam(team);
    }

    public void DeleteTeam(Team team)
    {
        _teamRepository.DeleteTeam(team);
    }
}