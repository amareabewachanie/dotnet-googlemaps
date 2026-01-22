namespace Dotnet.GoogleMaps.AerialView;

/// <summary>
/// Client for Google Maps Aerial View API
/// </summary>
public interface IAerialViewClient
{
    Task<object> RenderVideoAsync(object request, CancellationToken cancellationToken = default);
}
