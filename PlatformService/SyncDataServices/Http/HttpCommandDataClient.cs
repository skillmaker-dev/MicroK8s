using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _configuration = configuration;
    }
    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _http.PostAsync($"{_configuration["CommandService"]}/platforms", httpContent);

        if (response.IsSuccessStatusCode)
            Console.WriteLine("--> Sync POST to CommandService was Ok!");
        else Console.WriteLine("--> Sync POST to CommandService was NOT Ok!");
    }
}