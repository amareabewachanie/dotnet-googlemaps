namespace Dotnet.GoogleMaps.Geolocation;

/// <summary>
/// Implementation of Geolocation API client
/// </summary>
public class GeolocationClient : IGeolocationClient
{
    private readonly HttpClient _httpClient;

    public GeolocationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetLocationAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Geolocation API implementation coming soon");
    }
}
