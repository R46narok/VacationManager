using System.Collections.Generic;
using Core.Data.Entities;

namespace Models.SearchModel;

public class RoleSearch
{
    public string Name { get; set; }
    public List<Role> Roles { get; set; }
}