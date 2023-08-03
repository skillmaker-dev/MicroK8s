using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data;

public class PrebDb
{
    public static void PrepPopulation(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var grpcClient = scope.ServiceProvider.GetService<IPlatformDataClient>();

        var platforms = grpcClient.ReturnAllPlatforms();
        SeedData(scope.ServiceProvider.GetService<ICommandRepo>(), platforms);
    }

    private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
        Console.WriteLine("--> Seeding new platforms...");

        foreach (var plat in platforms)
        {
            if (!repo.ExternalPlatformExists(plat.ExternalID))
            {
                repo.CreatePlatform(plat);
            }


        }

        repo.SaveChanges();
    }
}
