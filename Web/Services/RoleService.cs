using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;

namespace Web.Services;

public class RoleService : IRoleService
{
    public readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public List<Role> GetRoles()
    {
        return _roleRepository.GetRoles().ToList();
    }

    public Role GetRole(string id)
    {
        return _roleRepository.GetRole(id);
    }

    public void AddRole(Role role)
    {
        _roleRepository.AddRole(role);
    }

    public void EditRole(Role role)
    {
        _roleRepository.EditRole(role);
    }

    public void DeleteRole(Role role)
    {
        _roleRepository.DeleteRole(role);
    }
}