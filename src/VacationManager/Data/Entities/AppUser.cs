using Microsoft.AspNetCore.Identity;

namespace VacationManager.Data.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public Team Team { get; set; }
}