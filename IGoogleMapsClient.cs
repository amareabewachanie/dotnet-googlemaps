using Dotnet.GoogleMaps.AddressValidation;
using Dotnet.GoogleMaps.Geocoding;
using Dotnet.GoogleMaps.Places;
using Dotnet.GoogleMaps.Routes;
using Dotnet.GoogleMaps.TimeZone;
using Dotnet.GoogleMaps.Elevation;
using Dotnet.GoogleMaps.Roads;
using Dotnet.GoogleMaps.StreetView;
using Dotnet.GoogleMaps.AerialView;
using Dotnet.GoogleMaps.Geolocation;
using Dotnet.GoogleMaps.Solar;
using Dotnet.GoogleMaps.Pollen;
using Dotnet.GoogleMaps.Weather;
using Dotnet.GoogleMaps.AirQuality;
using Dotnet.GoogleMaps.PlacesAggregate;

namespace Dotnet.GoogleMaps;

/// <summary>
/// Main interface for accessing all Google Maps Platform APIs
/// </summary>
public interface IGoogleMapsClient
{
    /// <summary>
    /// Address Validation API client
    /// </summary>
    IAddressValidationClient AddressValidation { get; }

    /// <summary>
    /// Geocoding API client
    /// </summary>
    IGeocodingClient Geocoding { get; }

    /// <summary>
    /// Places API client
    /// </summary>
    IPlacesClient Places { get; }

    /// <summary>
    /// Routes API client
    /// </summary>
    IRoutesClient Routes { get; }

    /// <summary>
    /// Time Zone API client
    /// </summary>
    ITimeZoneClient TimeZone { get; }

    /// <summary>
    /// Elevation API client
    /// </summary>
    IElevationClient Elevation { get; }

    /// <summary>
    /// Roads API client
    /// </summary>
    IRoadsClient Roads { get; }

    /// <summary>
    /// Street View API client
    /// </summary>
    IStreetViewClient StreetView { get; }

    /// <summary>
    /// Aerial View API client
    /// </summary>
    IAerialViewClient AerialView { get; }

    /// <summary>
    /// Geolocation API client
    /// </summary>
    IGeolocationClient Geolocation { get; }

    /// <summary>
    /// Solar API client
    /// </summary>
    ISolarClient Solar { get; }

    /// <summary>
    /// Pollen API client
    /// </summary>
    IPollenClient Pollen { get; }

    /// <summary>
    /// Weather API client
    /// </summary>
    IWeatherClient Weather { get; }

    /// <summary>
    /// Air Quality API client
    /// </summary>
    IAirQualityClient AirQuality { get; }

    /// <summary>
    /// Places Aggregate API client
    /// </summary>
    IPlacesAggregateClient PlacesAggregate { get; }
}
