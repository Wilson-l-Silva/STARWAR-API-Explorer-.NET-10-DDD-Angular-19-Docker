using System.Net.Http.Json;
using SwapiExplorer.Domain.Entities;
using SwapiExplorer.Domain.Repositories;
using SwapiExplorer.Infrastructure.ExternalModels;
using SwapiExplorer.Infrastructure.Helpers;

namespace SwapiExplorer.Infrastructure.Repositories;

// Implementação CONCRETA da interface definida no Domain.
// Esta classe é a única "ponte" entre o nosso sistema e a SWAPI real.
public class SwapiRepository : ISwapiRepository
{
    private readonly HttpClient _httpClient;

    // O HttpClient é injetado via HttpClientFactory (configurado no Program.cs da API).
    // Isso evita o problema clássico de "socket exhaustion" ao instanciar HttpClient manualmente.
    public SwapiRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<Planet>> GetPlanetsAsync(int page, CancellationToken cancellationToken = default)
    {
        // GetFromJsonAsync já faz a chamada HTTP GET e desserializa o JSON automaticamente.
        var response = await _httpClient.GetFromJsonAsync<SwapiListResponse<SwapiPlanetResponse>>(
            $"planets/?page={page}", cancellationToken);

        if (response is null) return Array.Empty<Planet>();

        // Mapeia cada item externo (SwapiPlanetResponse) para a entidade de Domínio (Planet)
        return response.Results
            .Select(r => Planet.Create(SwapiUrlHelper.ExtractId(r.Url), r.Name, r.Climate, r.Terrain, r.Population))
            .ToList();
    }

    public async Task<Planet?> GetPlanetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var r = await _httpClient.GetFromJsonAsync<SwapiPlanetResponse>($"planets/{id}/", cancellationToken);
            if (r is null) return null;

            return Planet.Create(id, r.Name, r.Climate, r.Terrain, r.Population);
        }
        catch (HttpRequestException)
        {
            // Se a SWAPI devolver 404, tratamos como "não encontrado" em vez de propagar exceção HTTP crua.
            return null;
        }
    }

    public async Task<IReadOnlyList<Character>> GetCharactersAsync(int page, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<SwapiListResponse<SwapiPeopleResponse>>(
            $"people/?page={page}", cancellationToken);

        if (response is null) return Array.Empty<Character>();

        var characters = new List<Character>();

        foreach (var r in response.Results)
        {
            var character = Character.Create(SwapiUrlHelper.ExtractId(r.Url), r.Name, r.BirthYear, r.Gender);

            // Enriquecimento: buscamos o nome do planeta natal (a SWAPI só devolve a URL).
            // Em um projeto real, isso seria cacheado para não gerar N chamadas extras.
            if (!string.IsNullOrWhiteSpace(r.Homeworld))
            {
                var homeworldId = SwapiUrlHelper.ExtractId(r.Homeworld);
                var homeworld = await GetPlanetByIdAsync(homeworldId, cancellationToken);
                if (homeworld is not null)
                    character.SetHomeworldName(homeworld.Name);
            }

            characters.Add(character);
        }

        return characters;
    }

    public async Task<Character?> GetCharacterByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var r = await _httpClient.GetFromJsonAsync<SwapiPeopleResponse>($"people/{id}/", cancellationToken);
            if (r is null) return null;

            var character = Character.Create(id, r.Name, r.BirthYear, r.Gender);

            if (!string.IsNullOrWhiteSpace(r.Homeworld))
            {
                var homeworldId = SwapiUrlHelper.ExtractId(r.Homeworld);
                var homeworld = await GetPlanetByIdAsync(homeworldId, cancellationToken);
                if (homeworld is not null)
                    character.SetHomeworldName(homeworld.Name);
            }

            return character;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}