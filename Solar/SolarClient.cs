namespace Dotnet.GoogleMaps.Solar;

/// <summary>
/// Implementation of Solar API client
/// </summary>
public class SolarClient : ISolarClient
{
    private readonly HttpClient _httpClient;

    public SolarClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetDataLayersAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Solar API implementation coming soon");
    }
}
