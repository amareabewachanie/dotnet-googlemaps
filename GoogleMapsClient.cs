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
using Microsoft.Extensions.Options;

namespace Dotnet.GoogleMaps;

/// <summary>
/// Main client for accessing all Google Maps Platform APIs
/// </summary>
public class GoogleMapsClient : IGoogleMapsClient
{
    public IAddressValidationClient AddressValidation { get; }
    public IGeocodingClient Geocoding { get; }
    public IPlacesClient Places { get; }
    public IRoutesClient Routes { get; }
    public ITimeZoneClient TimeZone { get; }
    public IElevationClient Elevation { get; }
    public IRoadsClient Roads { get; }
    public IStreetViewClient StreetView { get; }
    public IAerialViewClient AerialView { get; }
    public IGeolocationClient Geolocation { get; }
    public ISolarClient Solar { get; }
    public IPollenClient Pollen { get; }
    public IWeatherClient Weather { get; }
    public IAirQualityClient AirQuality { get; }
    public IPlacesAggregateClient PlacesAggregate { get; }

    public GoogleMapsClient(
        IAddressValidationClient addressValidation,
        IGeocodingClient geocoding,
        IPlacesClient places,
        IRoutesClient routes,
        ITimeZoneClient timeZone,
        IElevationClient elevation,
        IRoadsClient roads,
        IStreetViewClient streetView,
        IAerialViewClient aerialView,
        IGeolocationClient geolocation,
        ISolarClient solar,
        IPollenClient pollen,
        IWeatherClient weather,
        IAirQualityClient airQuality,
        IPlacesAggregateClient placesAggregate)
    {
        AddressValidation = addressValidation;
        Geocoding = geocoding;
        Places = places;
        Routes = routes;
        TimeZone = timeZone;
        Elevation = elevation;
        Roads = roads;
        StreetView = streetView;
        AerialView = aerialView;
        Geolocation = geolocation;
        Solar = solar;
        Pollen = pollen;
        Weather = weather;
        AirQuality = airQuality;
        PlacesAggregate = placesAggregate;
    }
}
