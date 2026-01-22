namespace Dotnet.GoogleMaps.Weather;

/// <summary>
/// Client for Google Maps Weather API
/// </summary>
public interface IWeatherClient
{
    Task<object> GetWeatherInfoAsync(object request, CancellationToken cancellationToken = default);
}
