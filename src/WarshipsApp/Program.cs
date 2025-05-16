using WarshipsApp.HealthChecks;
using WarshipsApp.InfrastructureLayer.Logging;

namespace WarshipsApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            // start app
            var cancellationToken = new CancellationToken();
            await host.StartAsync(cancellationToken);

            // signalize readiness probe
            var readinessProbe = host.Services.GetRequiredService<ReadinessHealthCheck>();
            readinessProbe.MarkReady();

            await host.WaitForShutdownAsync(cancellationToken);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureConsoleLogging()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddEnvironmentVariables();
                    });
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}