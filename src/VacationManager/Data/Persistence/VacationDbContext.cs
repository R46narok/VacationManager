using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VacationManager.Data.Entities;

namespace VacationManager.Data.Persistence;

public class VacationDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Project> Projects { get; set; } = null!; 
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Vacation> Vacations { get; set; } = null!;

    public VacationDbContext()
    {
        
    }

    public VacationDbContext(DbContextOptions<VacationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Vacation>()
            .Property(e => e.Type)
            .HasConversion(
            v => v.ToString(),
            v => (VacationType)Enum.Parse(typeof(VacationType), v));
    }
}