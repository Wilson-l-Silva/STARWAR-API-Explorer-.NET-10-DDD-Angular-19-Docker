using SwapiExplorer.Application.DTOs;
using SwapiExplorer.Application.Interfaces;
using SwapiExplorer.Domain.Repositories;

namespace SwapiExplorer.Application.Services;

// Implementação do caso de uso. Ela ORQUESTRA o domínio, mas não contém regra de negócio pesada
// (essa fica dentro das entidades, como vimos no Planet.IsPopulated()).
public class StarWarsAppService : IStarWarsAppService
{
    // Dependemos da ABSTRAÇÃO (interface do Domain), nunca da implementação concreta.
    // Isso é Injeção de Dependência (DI) + Inversão de Dependência.
    private readonly ISwapiRepository _swapiRepository;

    public StarWarsAppService(ISwapiRepository swapiRepository)
    {
        _swapiRepository = swapiRepository;
    }

    public async Task<IReadOnlyList<PlanetDto>> ListPlanetsAsync(int page, CancellationToken cancellationToken = default)
    {
        // 1. Busca as entidades de domínio através do repositório
        var planets = await _swapiRepository.GetPlanetsAsync(page, cancellationToken);

        // 2. Converte (mapeia) as entidades para DTOs, que é o formato exposto pela API
        return planets
            .Select(p => new PlanetDto(p.Id, p.Name, p.Climate, p.Terrain, p.Population, p.IsPopulated()))
            .ToList();
    }

    public async Task<PlanetDto?> GetPlanetAsync(int id, CancellationToken cancellationToken = default)
    {
        var planet = await _swapiRepository.GetPlanetByIdAsync(id, cancellationToken);

        // Se não encontrou, devolve null; o Controller decide o que fazer (ex: 404)
        if (planet is null) return null;

        return new PlanetDto(planet.Id, planet.Name, planet.Climate, planet.Terrain, planet.Population, planet.IsPopulated());
    }

    public async Task<IReadOnlyList<CharacterDto>> ListCharactersAsync(int page, CancellationToken cancellationToken = default)
    {
        var characters = await _swapiRepository.GetCharactersAsync(page, cancellationToken);

        return characters
            .Select(c => new CharacterDto(c.Id, c.Name, c.BirthYear, c.Gender, c.HomeworldName))
            .ToList();
    }

    public async Task<CharacterDto?> GetCharacterAsync(int id, CancellationToken cancellationToken = default)
    {
        var character = await _swapiRepository.GetCharacterByIdAsync(id, cancellationToken);
        if (character is null) return null;

        return new CharacterDto(character.Id, character.Name, character.BirthYear, character.Gender, character.HomeworldName);
    }
}