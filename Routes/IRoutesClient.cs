namespace Dotnet.GoogleMaps.Routes;

/// <summary>
/// Client for Google Maps Routes API
/// </summary>
public interface IRoutesClient
{
    // Placeholder - to be implemented
    Task<object> ComputeRoutesAsync(object request, CancellationToken cancellationToken = default);
}
