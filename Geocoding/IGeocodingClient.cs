namespace Dotnet.GoogleMaps.Geocoding;

/// <summary>
/// Client for Google Maps Geocoding API
/// </summary>
public interface IGeocodingClient
{
    /// <summary>
    /// Geocodes an address to coordinates
    /// </summary>
    Task<GeocodingResponse> GeocodeAsync(GeocodingRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reverse geocodes coordinates to an address
    /// </summary>
    Task<GeocodingResponse> ReverseGeocodeAsync(ReverseGeocodingRequest request, CancellationToken cancellationToken = default);
}
