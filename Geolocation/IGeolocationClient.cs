namespace Dotnet.GoogleMaps.Geolocation;

/// <summary>
/// Client for Google Maps Geolocation API
/// </summary>
public interface IGeolocationClient
{
    Task<object> GetLocationAsync(object request, CancellationToken cancellationToken = default);
}
