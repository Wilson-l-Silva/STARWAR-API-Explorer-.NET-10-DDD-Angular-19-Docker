using Microsoft.Extensions.DependencyInjection;
using SwapiExplorer.Application.Interfaces;
using SwapiExplorer.Application.Services;

namespace SwapiExplorer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Scoped: uma instância por requisição HTTP, o padrão recomendado para services de API.
        services.AddScoped<IStarWarsAppService, StarWarsAppService>();
        return services;
    }
}