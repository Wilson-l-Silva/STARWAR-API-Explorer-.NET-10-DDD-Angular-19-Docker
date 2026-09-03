using SwapiExplorer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwapiExplorer.Domain.Repositories
{
    // Este é o "contrato" que o Domain exige de quem for buscar dados externos.
    // O Domain não sabe (e não quer saber) que quem implementa isso faz uma chamada HTTP para swapi.dev.
    public interface ISwapiRepository
    {

        // Busca uma página de planetas
        Task<IReadOnlyList<Planet>> GetPlanetsAsync(int page, CancellationToken cancellationToken = default);

        // Busca um planeta específico por Id
        Task<Planet> GetPlanetByIdAsync(int id, CancellationToken cancellationToken = default);

        // Busca uma página de personagens
        Task<IReadOnlyList<Character>> GetCharactersAsync(int page, CancellationToken cancellation = default);

        // Busca um personagem específico por Id
        Task<Character> GetCharacterByIdAsync(int id, CancellationToken cancellationToken = default);

    }
}
