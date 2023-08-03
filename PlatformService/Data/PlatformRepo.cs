using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _dbContext;

    public PlatformRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreatePlatfrom(Platform platform)
    {
        if (platform is null)
            throw new ArgumentNullException(nameof(platform));

        _dbContext.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _dbContext.Platforms.ToList();
    }

    public Platform? GetPlatformById(int id)
    {
        return _dbContext.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() >= 0;
    }
}