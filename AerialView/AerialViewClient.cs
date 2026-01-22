namespace Dotnet.GoogleMaps.AerialView;

/// <summary>
/// Implementation of Aerial View API client
/// </summary>
public class AerialViewClient : IAerialViewClient
{
    private readonly HttpClient _httpClient;

    public AerialViewClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> RenderVideoAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Aerial View API implementation coming soon");
    }
}
