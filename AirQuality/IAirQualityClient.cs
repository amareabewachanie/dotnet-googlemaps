namespace Dotnet.GoogleMaps.AirQuality;

/// <summary>
/// Client for Google Maps Air Quality API
/// </summary>
public interface IAirQualityClient
{
    Task<object> GetAirQualityAsync(object request, CancellationToken cancellationToken = default);
}
