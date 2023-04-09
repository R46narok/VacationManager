using Core.Data;
using Core.Data.Entities;
using Core.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repositories.Helpers;

namespace VacationManager.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetService<VacationManagerDbContext>();
            db.Database.EnsureCreated();

            var repository = scope.ServiceProvider.GetService<ILoginRegisterRepository>();
            var userRepository = scope.ServiceProvider.GetService<IUserRepository>();

            var user = userRepository.GetUser("root");
            if (user is null)
                repository.Register(new User
                {
                    UserName = "root",
                    FirstName = "Root",
                    LastName = "Root",
                    PasswordHash = Hasher.Hash("rootpass123"),
                    RoleId = "1"
                });

            var teamlead = userRepository.GetUser("teamlead");
            if (teamlead is null)
                repository.Register(new User
                {
                    UserName = "teamlead",
                    FirstName = "Team Lead",
                    LastName = "Team Lead",
                    PasswordHash = Hasher.Hash("pass"),
                    RoleId = "3"
                });
            
            
            var dev = userRepository.GetUser("dev");
            if (dev is null)
                repository.Register(new User
                {
                    UserName = "dev",
                    FirstName = "Developerski",
                    LastName = "Developerski",
                    PasswordHash = Hasher.Hash("pass"),
                    RoleId = "2"
                });
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}