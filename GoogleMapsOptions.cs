namespace Dotnet.GoogleMaps;

/// <summary>
/// Configuration options for Google Maps API clients
/// </summary>
public class GoogleMapsOptions
{
    /// <summary>
    /// Google Maps Platform API Key
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Base URL for Google Maps APIs (default: https://maps.googleapis.com)
    /// </summary>
    public string BaseUrl { get; set; } = "https://maps.googleapis.com";

    /// <summary>
    /// Timeout for HTTP requests in seconds (default: 30)
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Retry and resiliency configuration
    /// </summary>
    public ResiliencyOptions Resiliency { get; set; } = new();
}

/// <summary>
/// Configuration options for retry and resiliency policies
/// </summary>
public class ResiliencyOptions
{
    /// <summary>
    /// Enable retry policy (default: true)
    /// </summary>
    public bool EnableRetry { get; set; } = true;

    /// <summary>
    /// Maximum number of retry attempts (default: 3)
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Base delay for exponential backoff in seconds (default: 1)
    /// </summary>
    public double RetryBaseDelaySeconds { get; set; } = 1.0;

    /// <summary>
    /// Maximum delay for exponential backoff in seconds (default: 30)
    /// </summary>
    public double RetryMaxDelaySeconds { get; set; } = 30.0;

    /// <summary>
    /// Enable circuit breaker (default: true)
    /// </summary>
    public bool EnableCircuitBreaker { get; set; } = true;

    /// <summary>
    /// Number of exceptions before opening circuit breaker (default: 5)
    /// </summary>
    public int CircuitBreakerFailureThreshold { get; set; } = 5;

    /// <summary>
    /// Duration in seconds that circuit breaker stays open (default: 30)
    /// </summary>
    public double CircuitBreakerDurationOfBreakSeconds { get; set; } = 30.0;

    /// <summary>
    /// Enable timeout policy (default: true)
    /// </summary>
    public bool EnableTimeout { get; set; } = true;

    /// <summary>
    /// Timeout duration in seconds (default: 30)
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// HTTP status codes that should trigger retry (default: 408, 429, 500, 502, 503, 504)
    /// </summary>
    public int[] RetryableStatusCodes { get; set; } = { 408, 429, 500, 502, 503, 504 };
}
