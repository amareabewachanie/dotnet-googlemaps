namespace Dotnet.GoogleMaps.PlacesAggregate;

/// <summary>
/// Implementation of Places Aggregate API client
/// </summary>
public class PlacesAggregateClient : IPlacesAggregateClient
{
    private readonly HttpClient _httpClient;

    public PlacesAggregateClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> ComputeInsightsAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Places Aggregate API implementation coming soon");
    }
}
