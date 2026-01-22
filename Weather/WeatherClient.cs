namespace Dotnet.GoogleMaps.Weather;

/// <summary>
/// Implementation of Weather API client
/// </summary>
public class WeatherClient : IWeatherClient
{
    private readonly HttpClient _httpClient;

    public WeatherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<object> GetWeatherInfoAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Weather API implementation coming soon");
    }
}
