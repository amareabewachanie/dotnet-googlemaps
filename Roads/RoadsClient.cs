namespace Dotnet.GoogleMaps.Roads;

/// <summary>
/// Implementation of Roads API client
/// </summary>
public class RoadsClient : IRoadsClient
{
    private readonly HttpClient _httpClient;

    public RoadsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> SnapToRoadsAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Roads API implementation coming soon");
    }
}
