namespace Dotnet.GoogleMaps.Places;

/// <summary>
/// Implementation of Places API client
/// </summary>
public class PlacesClient : IPlacesClient
{
    private readonly HttpClient _httpClient;

    public PlacesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> SearchNearbyAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Places API implementation coming soon");
    }
}
