using SwapiExplorer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwapiExplorer.Application.Interfaces;

// Contrato do "orquestrador" de casos de uso relacionados ao universo Star Wars.
// Os Controllers da API vão depender APENAS desta interface, nunca da implementação.
public interface IStarWarsAppService
{
    Task<IReadOnlyList<PlanetDto>> ListPlanetsAsync(int page, CancellationToken cancellationToken = default);
    Task<PlanetDto?> GetPlanetAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CharacterDto>> ListCharactersAsync(int page, CancellationToken cancellationToken = default);
    Task<CharacterDto?> GetCharacterAsync(int id, CancellationToken cancellationToken = default);

}
