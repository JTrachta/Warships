using Microsoft.Extensions.DependencyInjection;
using WarshipsApp.ApplicationLayer.Services;
using WarshipsApp.ApplicationLayer.Services.Abstractions;

namespace WarshipsApp.ApplicationLayer.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Register application layer services  
        services.AddSingleton<IGameService, GameService>();

        return services;
    }
}
