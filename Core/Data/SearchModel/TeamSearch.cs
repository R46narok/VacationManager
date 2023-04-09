using Core.Data.Entities;

namespace Core.Data.SearchModel;

public class TeamSearch
{
    public string Name { get; set; }

    public string TeamLeadNames { get; set; }

    public List<Team> Results { get; set; }
}