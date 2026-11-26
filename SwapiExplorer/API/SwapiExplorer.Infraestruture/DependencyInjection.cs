using Microsoft.Extensions.DependencyInjection;
using SwapiExplorer.Domain.Repositories;
using SwapiExplorer.Infrastructure.Repositories;

namespace SwapiExplorer.Infrastructure;

// Método de extensão que centraliza TODO o registro de dependências da camada Infrastructure.
// Isso mantém o Program.cs da API limpo, com apenas uma linha de chamada.
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Registra o HttpClient TIPADO para o SwapiRepository, com a BaseAddress já configurada.
        services.AddHttpClient<ISwapiRepository, SwapiRepository>(client =>
        {
            client.BaseAddress = new Uri("https://swapi.dev/api/");
            client.Timeout = TimeSpan.FromSeconds(30);
            // Header recomendado por boas práticas: identifica quem está consumindo a API pública.
            client.DefaultRequestHeaders.Add("User-Agent", "SwapiExplorer-Tutorial/1.0");
        });

        return services;
    }
}