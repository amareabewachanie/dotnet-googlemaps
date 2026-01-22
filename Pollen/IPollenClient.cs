namespace Dotnet.GoogleMaps.Pollen;

/// <summary>
/// Client for Google Maps Pollen API
/// </summary>
public interface IPollenClient
{
    Task<object> GetPollenInfoAsync(object request, CancellationToken cancellationToken = default);
}
