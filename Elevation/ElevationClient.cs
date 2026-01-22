namespace Dotnet.GoogleMaps.Elevation;

/// <summary>
/// Implementation of Elevation API client
/// </summary>
public class ElevationClient : IElevationClient
{
    private readonly HttpClient _httpClient;

    public ElevationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetElevationAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Elevation API implementation coming soon");
    }
}
