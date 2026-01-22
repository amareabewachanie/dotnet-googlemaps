namespace Dotnet.GoogleMaps.AirQuality;

/// <summary>
/// Implementation of Air Quality API client
/// </summary>
public class AirQualityClient : IAirQualityClient
{
    private readonly HttpClient _httpClient;

    public AirQualityClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetAirQualityAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Air Quality API implementation coming soon");
    }
}
