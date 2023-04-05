using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repositories.Helpers;
using Repositories.Interfaces;

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

            if (userRepository.GetUser("root") is null)
                repository.Register(new User
                {
                    UserName = "root",
                    FirstName = "Root",
                    LastName = "Root",
                    PasswordHash = Hasher.Hash("rootpass123"),
                    RoleId = "1"
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