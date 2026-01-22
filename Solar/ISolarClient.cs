namespace Dotnet.GoogleMaps.Solar;

/// <summary>
/// Client for Google Maps Solar API
/// </summary>
public interface ISolarClient
{
    Task<object> GetDataLayersAsync(object request, CancellationToken cancellationToken = default);
}
