using Core.Data.Entities;

namespace Core.Data.Repositories.Interfaces;

public interface IProjectRepository
{
    IQueryable<Project> GetProjects();
    Project GetProject(string id);
    void AddProject(Project project);
    void EditProject(Project project);
    void DeleteProject(Project project);
}