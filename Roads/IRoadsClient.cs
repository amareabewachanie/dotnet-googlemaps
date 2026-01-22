namespace Dotnet.GoogleMaps.Roads;

/// <summary>
/// Client for Google Maps Roads API
/// </summary>
public interface IRoadsClient
{
    Task<object> SnapToRoadsAsync(object request, CancellationToken cancellationToken = default);
}
