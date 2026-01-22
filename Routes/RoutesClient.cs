namespace Dotnet.GoogleMaps.Routes;

/// <summary>
/// Implementation of Routes API client
/// </summary>
public class RoutesClient : IRoutesClient
{
    private readonly HttpClient _httpClient;

    public RoutesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> ComputeRoutesAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Routes API implementation coming soon");
    }
}
