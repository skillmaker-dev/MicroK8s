using CommandsService.Models;

namespace CommandsService.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _dbContext;

    public CommandRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void CreateCommand(int platformId, Command command)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        command.PlatformId = platformId;
        _dbContext.Commands.Add(command);
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform is null)
            throw new ArgumentNullException(nameof(platform));

        _dbContext.Platforms.Add(platform);
    }

    public bool ExternalPlatformExists(int platformId)
    {
        return _dbContext.Platforms.Any(p => p.ExternalID == platformId);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _dbContext.Platforms;
    }

    public Command? GetCommand(int platformId, int commandId)
    {
        return _dbContext.Commands.FirstOrDefault(c => c.Id == commandId && c.PlatformId == platformId);
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return _dbContext.Commands.Where(c => c.PlatformId == platformId).OrderBy(p => p.Platform.Name);
    }

    public bool PlatformExists(int platformId)
    {
        return _dbContext.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() >= 0;
    }
}