using Core.Data.Entities;
using Core.Data.Repositories.Interfaces;

namespace Core.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly VacationManagerDbContext _context;

    public RoleRepository(VacationManagerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<Role> GetRoles()
    {
        return _context.Roles.AsQueryable();
    }

    public Role GetRole(string id)
    {
        return _context.Roles.Find(id);
    }

    public void AddRole(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    public void EditRole(Role role)
    {
        _context.Roles.Update(role);
        _context.SaveChanges();
    }

    public void DeleteRole(Role role)
    {
        _context.Roles.Remove(role);
        _context.SaveChanges();
    }
}