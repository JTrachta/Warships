using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WarshipsApp.ApplicationLayer.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}