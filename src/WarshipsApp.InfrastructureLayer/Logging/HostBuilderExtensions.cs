using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace WarshipsApp.InfrastructureLayer.Logging
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureConsoleLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, collection) =>
                collection.AddLogging());
        }
    }
}
