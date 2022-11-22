using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using VacationManager.Core;

namespace VacationManager.Data.Entities;

public class Team : EntityBase<int>
{
    [MaxLength(30)]
    public string Name { get; set; }
    public Project Project { get; set; }
    
    public AppUser Leader { get; set; }
    public List<AppUser> Developers { get; set; }
}