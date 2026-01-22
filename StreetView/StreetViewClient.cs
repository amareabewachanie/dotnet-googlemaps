namespace Dotnet.GoogleMaps.StreetView;

/// <summary>
/// Implementation of Street View API client
/// </summary>
public class StreetViewClient : IStreetViewClient
{
    private readonly HttpClient _httpClient;

    public StreetViewClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetMetadataAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Street View API implementation coming soon");
    }
}
