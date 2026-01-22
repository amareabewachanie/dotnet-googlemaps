using System.Text.Json.Serialization;

namespace Dotnet.GoogleMaps.Geocoding;

#region Request Models

public class GeocodingRequest
{
    public string Address { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? Language { get; set; }
    public Bounds? Bounds { get; set; }
    public string? Components { get; set; }
}

public class ReverseGeocodingRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Language { get; set; }
    public string? ResultType { get; set; }
    public string? LocationType { get; set; }
}

#endregion

#region Response Models

public class GeocodingResponse
{
    [JsonPropertyName("results")]
    public List<GeocodingResult>? Results { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; set; }
}

public class GeocodingResult
{
    [JsonPropertyName("address_components")]
    public List<AddressComponent>? AddressComponents { get; set; }

    [JsonPropertyName("formatted_address")]
    public string? FormattedAddress { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry? Geometry { get; set; }

    [JsonPropertyName("place_id")]
    public string? PlaceId { get; set; }

    [JsonPropertyName("plus_code")]
    public PlusCode? PlusCode { get; set; }

    [JsonPropertyName("types")]
    public List<string>? Types { get; set; }
}

public class AddressComponent
{
    [JsonPropertyName("long_name")]
    public string? LongName { get; set; }

    [JsonPropertyName("short_name")]
    public string? ShortName { get; set; }

    [JsonPropertyName("types")]
    public List<string>? Types { get; set; }
}

public class Geometry
{
    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("location_type")]
    public string? LocationType { get; set; }

    [JsonPropertyName("viewport")]
    public Bounds? Viewport { get; set; }

    [JsonPropertyName("bounds")]
    public Bounds? Bounds { get; set; }
}

public class Location
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}

public class Bounds
{
    [JsonPropertyName("northeast")]
    public Location? Northeast { get; set; }

    [JsonPropertyName("southwest")]
    public Location? Southwest { get; set; }
}

public class PlusCode
{
    [JsonPropertyName("global_code")]
    public string? GlobalCode { get; set; }

    [JsonPropertyName("compound_code")]
    public string? CompoundCode { get; set; }
}

#endregion
