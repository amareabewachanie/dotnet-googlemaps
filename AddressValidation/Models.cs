using System.Text.Json.Serialization;

namespace Dotnet.GoogleMaps.AddressValidation;

#region Request Models

public class ValidateAddressRequest
{
    [JsonPropertyName("address")]
    public PostalAddress? Address { get; set; }

    [JsonPropertyName("previousResponseId")]
    public string? PreviousResponseId { get; set; }

    [JsonPropertyName("enableUspsCass")]
    public bool? EnableUspsCass { get; set; }
}

public class ProvideValidationFeedbackRequest
{
    [JsonPropertyName("conclusion")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ValidationConclusion Conclusion { get; set; }

    [JsonPropertyName("responseId")]
    public string ResponseId { get; set; } = string.Empty;
}

#endregion

#region Response Models

public class ValidateAddressResponse
{
    [JsonPropertyName("result")]
    public ValidationResult? Result { get; set; }

    [JsonPropertyName("responseId")]
    public string? ResponseId { get; set; }
}

public class ProvideValidationFeedbackResponse
{
    // Empty response
}

public class ValidationResult
{
    [JsonPropertyName("verdict")]
    public AddressVerdict? Verdict { get; set; }

    [JsonPropertyName("address")]
    public ValidatedAddress? Address { get; set; }

    [JsonPropertyName("geocode")]
    public Geocode? Geocode { get; set; }

    [JsonPropertyName("metadata")]
    public AddressMetadata? Metadata { get; set; }

    [JsonPropertyName("uspsData")]
    public UspsData? UspsData { get; set; }
}

public class AddressVerdict
{
    [JsonPropertyName("inputGranularity")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AddressGranularity? InputGranularity { get; set; }

    [JsonPropertyName("validationGranularity")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AddressGranularity? ValidationGranularity { get; set; }

    [JsonPropertyName("geocodeGranularity")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AddressGranularity? GeocodeGranularity { get; set; }

    [JsonPropertyName("addressComplete")]
    public bool? AddressComplete { get; set; }

    [JsonPropertyName("hasInferredComponents")]
    public bool? HasInferredComponents { get; set; }

    [JsonPropertyName("hasUnconfirmedComponents")]
    public bool? HasUnconfirmedComponents { get; set; }

    [JsonPropertyName("hasReplacedComponents")]
    public bool? HasReplacedComponents { get; set; }
}

public class ValidatedAddress
{
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; set; }

    [JsonPropertyName("postalAddress")]
    public PostalAddress? PostalAddress { get; set; }

    [JsonPropertyName("addressComponents")]
    public List<AddressComponent>? AddressComponents { get; set; }

    [JsonPropertyName("missingComponentTypes")]
    public List<string>? MissingComponentTypes { get; set; }

    [JsonPropertyName("unconfirmedComponentTypes")]
    public List<string>? UnconfirmedComponentTypes { get; set; }

    [JsonPropertyName("unresolvedTokens")]
    public List<string>? UnresolvedTokens { get; set; }
}

public class PostalAddress
{
    [JsonPropertyName("regionCode")]
    public string? RegionCode { get; set; }

    [JsonPropertyName("languageCode")]
    public string? LanguageCode { get; set; }

    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("administrativeArea")]
    public string? AdministrativeArea { get; set; }

    [JsonPropertyName("locality")]
    public string? Locality { get; set; }

    [JsonPropertyName("sublocality")]
    public string? Sublocality { get; set; }

    [JsonPropertyName("addressLines")]
    public List<string>? AddressLines { get; set; }

    [JsonPropertyName("recipients")]
    public List<string>? Recipients { get; set; }

    [JsonPropertyName("organization")]
    public string? Organization { get; set; }
}

public class AddressComponent
{
    [JsonPropertyName("componentName")]
    public ComponentName? ComponentName { get; set; }

    [JsonPropertyName("componentType")]
    public string? ComponentType { get; set; }

    [JsonPropertyName("confirmationLevel")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ConfirmationLevel? ConfirmationLevel { get; set; }

    [JsonPropertyName("inferred")]
    public bool? Inferred { get; set; }

    [JsonPropertyName("spellCorrected")]
    public bool? SpellCorrected { get; set; }

    [JsonPropertyName("replaced")]
    public bool? Replaced { get; set; }

    [JsonPropertyName("unexpected")]
    public bool? Unexpected { get; set; }
}

public class ComponentName
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("languageCode")]
    public string? LanguageCode { get; set; }
}

public class Geocode
{
    [JsonPropertyName("location")]
    public LatLng? Location { get; set; }

    [JsonPropertyName("plusCode")]
    public PlusCode? PlusCode { get; set; }

    [JsonPropertyName("bounds")]
    public Bounds? Bounds { get; set; }

    [JsonPropertyName("placeId")]
    public string? PlaceId { get; set; }

    [JsonPropertyName("placeTypes")]
    public List<string>? PlaceTypes { get; set; }
}

public class LatLng
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

public class PlusCode
{
    [JsonPropertyName("globalCode")]
    public string? GlobalCode { get; set; }

    [JsonPropertyName("compoundCode")]
    public string? CompoundCode { get; set; }
}

public class Bounds
{
    [JsonPropertyName("low")]
    public LatLng? Low { get; set; }

    [JsonPropertyName("high")]
    public LatLng? High { get; set; }
}

public class AddressMetadata
{
    [JsonPropertyName("business")]
    public bool? Business { get; set; }

    [JsonPropertyName("poBox")]
    public bool? PoBox { get; set; }

    [JsonPropertyName("residential")]
    public bool? Residential { get; set; }
}

public class UspsData
{
    [JsonPropertyName("standardizedAddress")]
    public UspsStandardizedAddress? StandardizedAddress { get; set; }

    [JsonPropertyName("deliveryPointCode")]
    public string? DeliveryPointCode { get; set; }

    [JsonPropertyName("deliveryPointCheckDigit")]
    public string? DeliveryPointCheckDigit { get; set; }

    [JsonPropertyName("dpvConfirmation")]
    public string? DpvConfirmation { get; set; }

    [JsonPropertyName("dpvFootnote")]
    public string? DpvFootnote { get; set; }

    [JsonPropertyName("cmra")]
    public string? Cmra { get; set; }

    [JsonPropertyName("vacant")]
    public string? Vacant { get; set; }

    [JsonPropertyName("elotNumber")]
    public string? ElotNumber { get; set; }

    [JsonPropertyName("elotFlag")]
    public string? ElotFlag { get; set; }
}

public class UspsStandardizedAddress
{
    [JsonPropertyName("firstAddressLine")]
    public string? FirstAddressLine { get; set; }

    [JsonPropertyName("cityStateZipAddressLine")]
    public string? CityStateZipAddressLine { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("zipCode")]
    public string? ZipCode { get; set; }

    [JsonPropertyName("zipCodeExtension")]
    public string? ZipCodeExtension { get; set; }
}

#endregion

#region Enums

public enum ValidationConclusion
{
    VALIDATION_CONCLUSION_UNSPECIFIED,
    VALIDATED_VERSION_USED,
    USER_VERSION_USED,
    UNVALIDATED_VERSION_USED
}

public enum AddressGranularity
{
    SUB_PREMISE,
    PREMISE,
    PREMISE_PROXIMITY,
    BLOCK,
    ROUTE,
    OTHER
}

public enum ConfirmationLevel
{
    CONFIRMATION_LEVEL_UNSPECIFIED,
    CONFIRMED,
    UNCONFIRMED_BUT_PLAUSIBLE,
    UNCONFIRMED_AND_SUSPICIOUS
}

#endregion
