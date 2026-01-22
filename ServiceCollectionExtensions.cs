using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
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
/// Extension methods for registering Google Maps services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Google Maps Platform API clients to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configure">Action to configure Google Maps options</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddGoogleMaps(
        this IServiceCollection services,
        Action<GoogleMapsOptions>? configure = null)
    {
        if (configure != null)
        {
            services.Configure(configure);
        }

        if (configure != null)
        {
            services.Configure(configure);
        }

        // Helper method to add resiliency policies to HTTP clients
        void AddResiliencyPolicies<TClient, TImplementation>(IHttpClientBuilder builder)
            where TClient : class
            where TImplementation : class, TClient
        {
            builder.AddPolicyHandler((serviceProvider, request) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                var logger = loggerFactory?.CreateLogger<TImplementation>();
                return ResiliencyPolicyBuilder.BuildPolicy(options.Resiliency, logger);
            });
        }

        // Register HTTP clients for each API with resiliency policies
        var addressValidationBuilder = services.AddHttpClient<IAddressValidationClient, AddressValidationClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://addressvalidation.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IAddressValidationClient, AddressValidationClient>(addressValidationBuilder);

        var geocodingBuilder = services.AddHttpClient<IGeocodingClient, GeocodingClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });
        AddResiliencyPolicies<IGeocodingClient, GeocodingClient>(geocodingBuilder);

        var placesBuilder = services.AddHttpClient<IPlacesClient, PlacesClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://places.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IPlacesClient, PlacesClient>(placesBuilder);

        var routesBuilder = services.AddHttpClient<IRoutesClient, RoutesClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://routes.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IRoutesClient, RoutesClient>(routesBuilder);

        var timeZoneBuilder = services.AddHttpClient<ITimeZoneClient, TimeZoneClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });
        AddResiliencyPolicies<ITimeZoneClient, TimeZoneClient>(timeZoneBuilder);

        var elevationBuilder = services.AddHttpClient<IElevationClient, ElevationClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });
        AddResiliencyPolicies<IElevationClient, ElevationClient>(elevationBuilder);

        var roadsBuilder = services.AddHttpClient<IRoadsClient, RoadsClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });
        AddResiliencyPolicies<IRoadsClient, RoadsClient>(roadsBuilder);

        var streetViewBuilder = services.AddHttpClient<IStreetViewClient, StreetViewClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });
        AddResiliencyPolicies<IStreetViewClient, StreetViewClient>(streetViewBuilder);

        var aerialViewBuilder = services.AddHttpClient<IAerialViewClient, AerialViewClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://aerialview.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IAerialViewClient, AerialViewClient>(aerialViewBuilder);

        var geolocationBuilder = services.AddHttpClient<IGeolocationClient, GeolocationClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });
        AddResiliencyPolicies<IGeolocationClient, GeolocationClient>(geolocationBuilder);

        var solarBuilder = services.AddHttpClient<ISolarClient, SolarClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://solar.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<ISolarClient, SolarClient>(solarBuilder);

        var pollenBuilder = services.AddHttpClient<IPollenClient, PollenClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://pollen.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IPollenClient, PollenClient>(pollenBuilder);

        var weatherBuilder = services.AddHttpClient<IWeatherClient, WeatherClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://weather.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IWeatherClient, WeatherClient>(weatherBuilder);

        var airQualityBuilder = services.AddHttpClient<IAirQualityClient, AirQualityClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://airquality.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IAirQualityClient, AirQualityClient>(airQualityBuilder);

        var placesAggregateBuilder = services.AddHttpClient<IPlacesAggregateClient, PlacesAggregateClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<GoogleMapsOptions>>().Value;
            client.BaseAddress = new Uri("https://areainsights.googleapis.com");
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", opts.ApiKey);
        });
        AddResiliencyPolicies<IPlacesAggregateClient, PlacesAggregateClient>(placesAggregateBuilder);

        // Register the main client
        services.AddScoped<IGoogleMapsClient, GoogleMapsClient>();

        return services;
    }
}
