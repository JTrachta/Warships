using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WarshipsApp.HealthChecks
{
    internal class ReadinessHealthCheck : IHealthCheck
    {
        private readonly ILogger<ReadinessHealthCheck> _logger;
        private volatile bool _startupTaskCompleted = false;

        public ReadinessHealthCheck(ILogger<ReadinessHealthCheck> logger)
        {
            _logger = logger;
        }

        public void MarkReady()
        {
            _startupTaskCompleted = true;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Readiness health check executed.");

            var result = _startupTaskCompleted
                ? HealthCheckResult.Healthy("The startup task is finished.")
                : HealthCheckResult.Unhealthy("The startup task is still running.");

            return Task.FromResult(result);
        }
    }
}
