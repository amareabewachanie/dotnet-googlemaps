namespace Dotnet.GoogleMaps.Pollen;

/// <summary>
/// Implementation of Pollen API client
/// </summary>
public class PollenClient : IPollenClient
{
    private readonly HttpClient _httpClient;

    public PollenClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetPollenInfoAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Pollen API implementation coming soon");
    }
}
