using Microsoft.Extensions.DependencyInjection;
using WarshipsApp.InterfaceLayer.Mapping;

namespace WarshipsApp.InterfaceLayer.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddInterfaceServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(WarshipsAppMappingProfile).Assembly);
            return services;
        }
    }
}
