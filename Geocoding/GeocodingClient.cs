using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Dotnet.GoogleMaps;

namespace Dotnet.GoogleMaps.Geocoding;

/// <summary>
/// Implementation of Geocoding API client
/// </summary>
public class GeocodingClient : IGeocodingClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly GoogleMapsOptions _options;

    public GeocodingClient(HttpClient httpClient, IOptions<GoogleMapsOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<GeocodingResponse> GeocodeAsync(GeocodingRequest request, CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>
        {
            $"address={Uri.EscapeDataString(request.Address)}"
        };
        
        if (!string.IsNullOrEmpty(request.Region))
            queryParams.Add($"region={Uri.EscapeDataString(request.Region)}");
        if (!string.IsNullOrEmpty(request.Language))
            queryParams.Add($"language={Uri.EscapeDataString(request.Language)}");
        if (!string.IsNullOrEmpty(request.Components))
            queryParams.Add($"components={Uri.EscapeDataString(request.Components)}");
        
        queryParams.Add($"key={Uri.EscapeDataString(_options.ApiKey)}");

        var url = $"/maps/api/geocode/json?{string.Join("&", queryParams)}";
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GeocodingResponse>(_jsonOptions, cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    public async Task<GeocodingResponse> ReverseGeocodeAsync(ReverseGeocodingRequest request, CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>
        {
            $"latlng={request.Latitude},{request.Longitude}"
        };
        
        if (!string.IsNullOrEmpty(request.Language))
            queryParams.Add($"language={Uri.EscapeDataString(request.Language)}");
        if (!string.IsNullOrEmpty(request.ResultType))
            queryParams.Add($"result_type={Uri.EscapeDataString(request.ResultType)}");
        if (!string.IsNullOrEmpty(request.LocationType))
            queryParams.Add($"location_type={Uri.EscapeDataString(request.LocationType)}");

        queryParams.Add($"key={Uri.EscapeDataString(_options.ApiKey)}");

        var url = $"/maps/api/geocode/json?{string.Join("&", queryParams)}";
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GeocodingResponse>(_jsonOptions, cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}
