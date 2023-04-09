using Core.Data.Entities;
using Core.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly VacationManagerDbContext _context;

    public ProjectRepository(VacationManagerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<Project> GetProjects()
    {
        return _context.Projects.AsQueryable();
    }

    public Project GetProject(string id)
    {
        return _context.Projects.Include(x => x.Teams).SingleOrDefault(x => x.Id == id);
    }

    public void AddProject(Project project)
    {
        _context.Projects.Add(project);
        _context.SaveChanges();
    }

    public void EditProject(Project project)
    {
        _context.Projects.Update(project);
        _context.SaveChanges();
    }

    public void DeleteProject(Project project)
    {
        _context.Projects.Remove(project);
        _context.SaveChanges();
    }
}