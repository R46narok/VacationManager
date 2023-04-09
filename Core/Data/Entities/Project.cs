namespace Core.Data.Entities;

public class Project : BaseEntity<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Team> Teams { get; set; }
}