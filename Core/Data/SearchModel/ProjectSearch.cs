using Core.Data.Entities;

namespace Core.Data.SearchModel;

public class ProjectSearch
{
    public string Name { get; set; }

    public string Description { get; set; }

    public List<Project> Results { get; set; }
}