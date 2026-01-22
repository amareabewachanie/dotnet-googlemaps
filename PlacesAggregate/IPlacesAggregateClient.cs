namespace Dotnet.GoogleMaps.PlacesAggregate;

/// <summary>
/// Client for Google Maps Places Aggregate API
/// </summary>
public interface IPlacesAggregateClient
{
    Task<object> ComputeInsightsAsync(object request, CancellationToken cancellationToken = default);
}
