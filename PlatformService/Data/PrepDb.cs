using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;
public static class PrepDb
{
    public static void PrepPopulation(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        if (dbContext is not null)
            SeedData(dbContext, app);

    }

    private static void SeedData(AppDbContext dbContext, WebApplication app)
    {
        if (app.Environment.IsProduction())
        {
            Console.WriteLine("--> Attemting to apply migrations...");
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }
        if (!dbContext.Platforms.Any())
        {
            Console.WriteLine("--> Seeding Data...");

            dbContext.Platforms.AddRange(
                new Platform()
                {
                    Name = "Dotnet",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Platform()
                {
                    Name = "Sql Server Express",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Platform()
                {
                    Name = "Kubernetes",
                    Publisher = "Cloud Native Computing Foundation",
                    Cost = "Free"
                }
            );

            dbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}
