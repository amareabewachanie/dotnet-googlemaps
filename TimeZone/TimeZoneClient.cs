namespace Dotnet.GoogleMaps.TimeZone;

/// <summary>
/// Implementation of Time Zone API client
/// </summary>
public class TimeZoneClient : ITimeZoneClient
{
    private readonly HttpClient _httpClient;

    public TimeZoneClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetTimeZoneAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Time Zone API implementation coming soon");
    }
}
