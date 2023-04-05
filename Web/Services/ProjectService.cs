using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;

namespace Web.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public List<Project> GetProjects()
    {
        return _projectRepository.GetProjects().Include(x => x.Teams).ToList();
    }

    public Project GetProject(string id)
    {
        return GetProjects().Find(x => x.Id == id);
    }

    public void AddProject(Project project)
    {
        _projectRepository.AddProject(project);
    }

    public void AddTeamToProject(Project project, Team team)
    {
        project.Teams.Add(team);
        _projectRepository.EditProject(project);
    }

    public void RemoveTeamFromProject(Project project, Team team)
    {
        project.Teams.Remove(team);
        _projectRepository.EditProject(project);
    }

    public void EditProject(Project project)
    {
        _projectRepository.EditProject(project);
    }

    public void DeleteProject(Project project)
    {
        _projectRepository.DeleteProject(project);
    }
}