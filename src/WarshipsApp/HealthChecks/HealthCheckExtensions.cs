using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace WarshipsApp.HealthChecks
{
    public static class HealthCheckExtensions
    {
        private const string ReadinessTag = "Readiness";

        public static void AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddSingleton<ReadinessHealthCheck>();
            services.AddHealthChecks()
                .AddCheck<ReadinessHealthCheck>("Readiness", HealthStatus.Unhealthy, [ReadinessTag]);
        }

        public static void UseCustomHealthChecks(this IApplicationBuilder app)
        {
            app.Map(new PathString("/health/ready"), appBuilder => {
                appBuilder.UseMiddleware<HealthCheckMiddleware>(Options.Create(new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains(ReadinessTag),
                    AllowCachingResponses = false
                }));
            });
        }
    }
}