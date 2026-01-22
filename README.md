# Dotnet.GoogleMaps

A comprehensive .NET wrapper for Google Maps Platform Core APIs. This NuGet package provides strongly-typed, async/await-friendly clients for all major Google Maps Platform APIs.

## Features

### API Clients

- ✅ **Address Validation API** - Validate and standardize addresses
- ✅ **Geocoding API** - Convert addresses to coordinates and vice versa
- 🔄 **Places API** - Search for places, get place details (coming soon)
- 🔄 **Routes API** - Calculate routes and directions (coming soon)
- 🔄 **Time Zone API** - Get time zone information (coming soon)
- 🔄 **Elevation API** - Get elevation data (coming soon)
- 🔄 **Roads API** - Snap points to roads (coming soon)
- 🔄 **Street View API** - Get Street View metadata (coming soon)
- 🔄 **Aerial View API** - Render aerial view videos (coming soon)
- 🔄 **Geolocation API** - Get location from cell towers/WiFi (coming soon)
- 🔄 **Solar API** - Get solar potential data (coming soon)
- 🔄 **Pollen API** - Get pollen information (coming soon)
- 🔄 **Weather API** - Get weather information (coming soon)
- 🔄 **Air Quality API** - Get air quality data (coming soon)
- 🔄 **Places Aggregate API** - Get area insights (coming soon)

### Resiliency & Reliability

- ✅ **Automatic Retry** - Exponential backoff retry for transient failures
- ✅ **Circuit Breaker** - Prevents cascading failures
- ✅ **Timeout Protection** - Configurable request timeouts
- ✅ **Comprehensive Logging** - Built-in logging for all resiliency events
- ✅ **Configurable Policies** - Fully customizable retry, circuit breaker, and timeout settings

## Installation

```bash
dotnet add package Axumite.GoogleMaps
```

## Quick Start

### 1. Register Services

In your `Program.cs` or `Startup.cs`:

```csharp
using Axumite.GoogleMaps;

var builder = WebApplication.CreateBuilder(args);

// Add Google Maps services
builder.Services.AddGoogleMaps(options =>
{
    options.ApiKey = builder.Configuration["GoogleMaps:ApiKey"];
    options.BaseUrl = "https://maps.googleapis.com";
    options.TimeoutSeconds = 30;
});

var app = builder.Build();
```

### 2. Use the Client

Inject `IGoogleMapsClient` into your services:

```csharp
using Axumite.GoogleMaps;
using Axumite.GoogleMaps.AddressValidation;
using Axumite.GoogleMaps.Geocoding;

public class MyService
{
    private readonly IGoogleMapsClient _googleMaps;

    public MyService(IGoogleMapsClient googleMaps)
    {
        _googleMaps = googleMaps;
    }

    public async Task ValidateAddressAsync()
    {
        var request = new ValidateAddressRequest
        {
            Address = new PostalAddress
            {
                RegionCode = "US",
                Locality = "Mountain View",
                AddressLines = new List<string> { "1600 Amphitheatre Pkwy" }
            }
        };

        var response = await _googleMaps.AddressValidation.ValidateAddressAsync(request);
      
        if (response.Result?.Verdict?.AddressComplete == true)
        {
            Console.WriteLine($"Validated Address: {response.Result.Address?.FormattedAddress}");
        }
    }

    public async Task GeocodeAddressAsync()
    {
        var request = new GeocodingRequest
        {
            Address = "1600 Amphitheatre Parkway, Mountain View, CA"
        };

        var response = await _googleMaps.Geocoding.GeocodeAsync(request);
      
        if (response.Results?.Any() == true)
        {
            var location = response.Results[0].Geometry?.Location;
            Console.WriteLine($"Lat: {location?.Lat}, Lng: {location?.Lng}");
        }
    }

    public async Task ReverseGeocodeAsync()
    {
        var request = new ReverseGeocodingRequest
        {
            Latitude = 37.422535,
            Longitude = -122.0847281
        };

        var response = await _googleMaps.Geocoding.ReverseGeocodeAsync(request);
      
        if (response.Results?.Any() == true)
        {
            Console.WriteLine($"Address: {response.Results[0].FormattedAddress}");
        }
    }
}
```

## Configuration

### appsettings.json

```json
{
  "GoogleMaps": {
    "ApiKey": "YOUR_API_KEY_HERE",
    "BaseUrl": "https://maps.googleapis.com",
    "TimeoutSeconds": 30,
    "Resiliency": {
      "EnableRetry": true,
      "MaxRetryAttempts": 3,
      "RetryBaseDelaySeconds": 1.0,
      "RetryMaxDelaySeconds": 30.0,
      "EnableCircuitBreaker": true,
      "CircuitBreakerFailureThreshold": 5,
      "CircuitBreakerDurationOfBreakSeconds": 30.0,
      "EnableTimeout": true,
      "TimeoutSeconds": 30,
      "RetryableStatusCodes": [ 408, 429, 500, 502, 503, 504 ]
    }
  }
}
```

### Using Configuration

```csharp
builder.Services.AddGoogleMaps(options =>
{
    options.ApiKey = builder.Configuration["GoogleMaps:ApiKey"];
    options.BaseUrl = builder.Configuration["GoogleMaps:BaseUrl"] ?? "https://maps.googleapis.com";
    options.TimeoutSeconds = builder.Configuration.GetValue<int>("GoogleMaps:TimeoutSeconds", 30);
  
    // Configure resiliency
    options.Resiliency.EnableRetry = true;
    options.Resiliency.MaxRetryAttempts = 3;
    options.Resiliency.RetryBaseDelaySeconds = 1.0;
    options.Resiliency.RetryMaxDelaySeconds = 30.0;
  
    options.Resiliency.EnableCircuitBreaker = true;
    options.Resiliency.CircuitBreakerFailureThreshold = 5;
    options.Resiliency.CircuitBreakerDurationOfBreakSeconds = 30.0;
  
    options.Resiliency.EnableTimeout = true;
    options.Resiliency.TimeoutSeconds = 30;
});
```

## Retry and Resiliency Features

The package includes built-in retry and resiliency features powered by [Polly](https://github.com/App-vNext/Polly) to handle transient failures gracefully:

### Retry Policy

- **Exponential Backoff**: Automatically retries failed requests with exponential backoff
- **Configurable Attempts**: Set the maximum number of retry attempts (default: 3)
- **Smart Retry**: Only retries on transient HTTP errors (408, 429, 500, 502, 503, 504) and network failures
- **Logging**: All retry attempts are logged for monitoring

### Circuit Breaker

- **Failure Threshold**: Opens circuit after a configurable number of failures (default: 5)
- **Auto-Recovery**: Automatically attempts to close the circuit after a duration (default: 30 seconds)
- **Half-Open State**: Tests connection before fully closing the circuit
- **Protection**: Prevents cascading failures by stopping requests when the service is down

### Timeout Policy

- **Request Timeout**: Automatically cancels requests that exceed the timeout duration
- **Configurable**: Set timeout per request (default: 30 seconds)
- **Prevents Hanging**: Ensures requests don't hang indefinitely

### Example: Custom Resiliency Configuration

```csharp
builder.Services.AddGoogleMaps(options =>
{
    options.ApiKey = "YOUR_API_KEY";
  
    // Aggressive retry for critical operations
    options.Resiliency.EnableRetry = true;
    options.Resiliency.MaxRetryAttempts = 5;
    options.Resiliency.RetryBaseDelaySeconds = 0.5;
    options.Resiliency.RetryMaxDelaySeconds = 10.0;
  
    // Strict circuit breaker
    options.Resiliency.EnableCircuitBreaker = true;
    options.Resiliency.CircuitBreakerFailureThreshold = 3;
    options.Resiliency.CircuitBreakerDurationOfBreakSeconds = 60.0;
  
    // Quick timeout for fast-fail scenarios
    options.Resiliency.EnableTimeout = true;
    options.Resiliency.TimeoutSeconds = 10;
  
    // Custom retryable status codes
    options.Resiliency.RetryableStatusCodes = new[] { 408, 429, 500, 502, 503, 504, 520, 521, 522, 524 };
});
```

### Disabling Resiliency Features

You can disable individual resiliency features if needed:

```csharp
builder.Services.AddGoogleMaps(options =>
{
    options.ApiKey = "YOUR_API_KEY";
  
    // Disable retry (not recommended for production)
    options.Resiliency.EnableRetry = false;
  
    // Disable circuit breaker
    options.Resiliency.EnableCircuitBreaker = false;
  
    // Disable timeout (not recommended)
    options.Resiliency.EnableTimeout = false;
});
```

## API Documentation

### Address Validation API

Validate and standardize addresses with support for USPS CASS validation (US addresses).

```csharp
var request = new ValidateAddressRequest
{
    Address = new PostalAddress
    {
        RegionCode = "US",
        Locality = "Mountain View",
        AddressLines = new List<string> { "1600 Amphitheatre Pkwy" }
    },
    EnableUspsCass = true // Enable USPS CASS validation for US addresses
};

var response = await _googleMaps.AddressValidation.ValidateAddressAsync(request);
```

### Geocoding API

Convert addresses to coordinates (geocoding) and coordinates to addresses (reverse geocoding).

```csharp
// Geocode an address
var geocodeRequest = new GeocodingRequest
{
    Address = "1600 Amphitheatre Parkway, Mountain View, CA",
    Language = "en"
};

var geocodeResponse = await _googleMaps.Geocoding.GeocodeAsync(geocodeRequest);

// Reverse geocode coordinates
var reverseRequest = new ReverseGeocodingRequest
{
    Latitude = 37.422535,
    Longitude = -122.0847281,
    Language = "en"
};

var reverseResponse = await _googleMaps.Geocoding.ReverseGeocodeAsync(reverseRequest);
```

## Individual API Clients

You can also inject individual API clients directly:

```csharp
public class MyService
{
    private readonly IAddressValidationClient _addressValidation;
    private readonly IGeocodingClient _geocoding;

    public MyService(
        IAddressValidationClient addressValidation,
        IGeocodingClient geocoding)
    {
        _addressValidation = addressValidation;
        _geocoding = geocoding;
    }
}
```

## Error Handling

All API calls throw `HttpRequestException` on HTTP errors. Handle them appropriately:

```csharp
try
{
    var response = await _googleMaps.AddressValidation.ValidateAddressAsync(request);
}
catch (HttpRequestException ex)
{
    // Handle HTTP errors (network issues, API errors, etc.)
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Requirements

- .NET 10.0 or later
- Google Maps Platform API Key

## Getting an API Key

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select an existing one
3. Enable the APIs you want to use
4. Create credentials (API Key)
5. Restrict the API key to specific APIs and applications for security

## License

MIT License

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

For issues and questions, please open an issue on GitHub.

## Roadmap

- [X] Address Validation API
- [X] Geocoding API
- [X] Places API (full implementation)
- [X] Routes API (full implementation)
- [X] Time Zone API (full implementation)
- [X] Elevation API (full implementation)
- [X] Roads API (full implementation)
- [X] Street View API (full implementation)
- [X] Aerial View API (full implementation)
- [X] Geolocation API (full implementation)
- [X] Solar API (full implementation)
- [X] Pollen API (full implementation)
- [X] Weather API (full implementation)
- [X] Air Quality API (full implementation)
- [X] Places Aggregate API (full implementation)
