using Core.Data.Entities;

namespace Core.Data.Repositories.Interfaces;

public interface IRoleRepository
{
    IQueryable<Role> GetRoles();
    Role GetRole(string id);
    void AddRole(Role role);
    void EditRole(Role role);
    void DeleteRole(Role role);
}