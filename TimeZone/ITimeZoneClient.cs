namespace Dotnet.GoogleMaps.TimeZone;

/// <summary>
/// Client for Google Maps Time Zone API
/// </summary>
public interface ITimeZoneClient
{
    Task<object> GetTimeZoneAsync(double latitude, double longitude, CancellationToken cancellationToken = default);
}
