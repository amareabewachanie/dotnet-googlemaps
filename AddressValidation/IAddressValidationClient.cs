namespace Dotnet.GoogleMaps.AddressValidation;

/// <summary>
/// Client for Google Maps Address Validation API
/// </summary>
public interface IAddressValidationClient
{
    /// <summary>
    /// Validates an address and returns standardized address information
    /// </summary>
    Task<ValidateAddressResponse> ValidateAddressAsync(ValidateAddressRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Provides feedback on address validation results
    /// </summary>
    Task<ProvideValidationFeedbackResponse> ProvideValidationFeedbackAsync(ProvideValidationFeedbackRequest request, CancellationToken cancellationToken = default);
}
