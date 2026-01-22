namespace Dotnet.GoogleMaps.StreetView;

/// <summary>
/// Client for Google Maps Street View API
/// </summary>
public interface IStreetViewClient
{
    Task<object> GetMetadataAsync(object request, CancellationToken cancellationToken = default);
}
