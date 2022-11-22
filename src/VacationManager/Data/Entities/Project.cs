using System.ComponentModel.DataAnnotations;
using VacationManager.Core;

namespace VacationManager.Data.Entities;

public class Project : EntityBase<int>
{
    [MaxLength(50)]
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Team Team { get; set; }
}