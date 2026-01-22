using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using System.Net;

namespace Dotnet.GoogleMaps;

/// <summary>
/// Helper class for building resiliency policies
/// </summary>
internal static class ResiliencyPolicyBuilder
{
    /// <summary>
    /// Builds a combined resiliency policy with retry, circuit breaker, and timeout
    /// </summary>
    public static IAsyncPolicy<HttpResponseMessage> BuildPolicy(ResiliencyOptions options, ILogger? logger = null)
    {
        var policies = new List<IAsyncPolicy<HttpResponseMessage>>();

        // Retry Policy
        if (options.EnableRetry)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => options.RetryableStatusCodes.Contains((int)msg.StatusCode))
                .WaitAndRetryAsync(
                    retryCount: options.MaxRetryAttempts,
                    sleepDurationProvider: retryAttempt =>
                    {
                        var delay = TimeSpan.FromSeconds(
                            Math.Min(
                                options.RetryBaseDelaySeconds * Math.Pow(2, retryAttempt - 1),
                                options.RetryMaxDelaySeconds
                            )
                        );
                        logger?.LogWarning(
                            "Retrying request. Attempt {Attempt} of {MaxAttempts} after {Delay} seconds",
                            retryAttempt,
                            options.MaxRetryAttempts,
                            delay.TotalSeconds
                        );
                        return delay;
                    },
                    onRetry: (outcome, timespan, retryCount, context) =>
                    {
                        logger?.LogWarning(
                            "Retry {RetryCount} after {Delay} seconds. Outcome: {Outcome}",
                            retryCount,
                            timespan.TotalSeconds,
                            outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()
                        );
                    }
                );

            policies.Add(retryPolicy);
        }

        // Circuit Breaker Policy
        if (options.EnableCircuitBreaker)
        {
            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => options.RetryableStatusCodes.Contains((int)msg.StatusCode))
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: options.CircuitBreakerFailureThreshold,
                    durationOfBreak: TimeSpan.FromSeconds(options.CircuitBreakerDurationOfBreakSeconds),
                    onBreak: (result, duration) =>
                    {
                        var errorMessage = result.Exception?.Message 
                            ?? result.Result?.StatusCode.ToString() 
                            ?? "Unknown error";
                        logger?.LogError(
                            "Circuit breaker opened for {Duration} seconds. Error: {Error}",
                            duration.TotalSeconds,
                            errorMessage
                        );
                    },
                    onReset: () =>
                    {
                        logger?.LogInformation("Circuit breaker reset");
                    },
                    onHalfOpen: () =>
                    {
                        logger?.LogInformation("Circuit breaker half-open, testing connection");
                    }
                );

            policies.Add(circuitBreakerPolicy);
        }

        // Timeout Policy
        if (options.EnableTimeout)
        {
            var timeoutPolicy = Policy
                .TimeoutAsync<HttpResponseMessage>(
                    TimeSpan.FromSeconds(options.TimeoutSeconds),
                    TimeoutStrategy.Pessimistic,
                    onTimeoutAsync: (context, timespan, task) =>
                    {
                        logger?.LogWarning(
                            "Request timed out after {Timeout} seconds",
                            timespan.TotalSeconds
                        );
                        return Task.CompletedTask;
                    }
                );

            policies.Add(timeoutPolicy);
        }

        // Combine all policies
        if (policies.Count == 0)
        {
            return Policy.NoOpAsync<HttpResponseMessage>();
        }

        return policies.Count == 1
            ? policies[0]
            : Policy.WrapAsync(policies.ToArray());
    }
}
