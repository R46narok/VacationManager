using System.Collections.Generic;
using Models;

namespace ViewModels.DTO;

public class ProjectDTO
{
    public string Name { get; set; }

    public string Description { get; set; }

    public List<Team> Teams { get; set; }
}