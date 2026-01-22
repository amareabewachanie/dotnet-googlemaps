namespace Dotnet.GoogleMaps.Elevation;

/// <summary>
/// Client for Google Maps Elevation API
/// </summary>
public interface IElevationClient
{
    Task<object> GetElevationAsync(object request, CancellationToken cancellationToken = default);
}
