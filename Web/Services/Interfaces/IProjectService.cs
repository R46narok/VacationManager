using System.Collections.Generic;
using Models;

namespace Web.Services.Interfaces;

public interface IProjectService
{
    List<Project> GetProjects();
    Project GetProject(string id);
    void AddProject(Project project);
    void AddTeamToProject(Project project, Team team);
    void RemoveTeamFromProject(Project project, Team team);
    void EditProject(Project project);
    void DeleteProject(Project project);
}