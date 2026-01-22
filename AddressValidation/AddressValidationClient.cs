using System.Net.Http.Json;
using System.Text.Json;

namespace Dotnet.GoogleMaps.AddressValidation;

/// <summary>
/// Implementation of Address Validation API client
/// </summary>
public class AddressValidationClient : IAddressValidationClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public AddressValidationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    public async Task<ValidateAddressResponse> ValidateAddressAsync(ValidateAddressRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("v1:validateAddress", request, _jsonOptions, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ValidateAddressResponse>(_jsonOptions, cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    public async Task<ProvideValidationFeedbackResponse> ProvideValidationFeedbackAsync(ProvideValidationFeedbackRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("v1:provideValidationFeedback", request, _jsonOptions, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProvideValidationFeedbackResponse>(_jsonOptions, cancellationToken)
            ?? new ProvideValidationFeedbackResponse();
    }
}
