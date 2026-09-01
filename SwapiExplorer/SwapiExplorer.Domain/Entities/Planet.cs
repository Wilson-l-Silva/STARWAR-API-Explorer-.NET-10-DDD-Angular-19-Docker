using System;
using System.Collections.Generic;
using System.Text;

namespace SwapiExplorer.Domain.Entities
{

    // Entidade: representa um Planeta do universo Star Wars dentro do NOSSO domínio.
    // Note que essa classe não sabe nada sobre SWAPI, JSON ou HTTP.
    public class Planet
    {
        // Identificador único do planeta (extraído da URL da SWAPI, ex: .../planets/1/ -> Id = 1)
        public int Id { get; set; }

        // Nome do planeta, ex: "Tatooine"
        public string Name { get; private set; }

        // Clima do planeta, ex: "arid"
        public string Climate { get; private set; }

        // Terreno, ex: "desert"
        public string Terrain { get; private set; }

        // População, guardada como string porque a SWAPI retorna "unknown" em alguns casos
        public string Population { get; private set; }

        // Construtor privado: força a criação via método de fábrica "Create",
        // garantindo que o objeto nunca exista em estado inválido (regra de negócio do DDD).
        public Planet(int id, string name, string climate, string terrain, string population)
        {
            Id = id;
            Name = name;
            Climate = climate;
            Terrain = terrain;
            Population = population;
        }

        // Factory Method: ponto único de criação da entidade, com validação de negócio.
        public static Planet Create(int id, string name, string climate, string terrain, string population)
        {
            // Regra de negócio simples: todo planeta precisa ter nome.
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("O nome do planeta é obrigatório", nameof(name));

            return new Planet(
                id,
                name,
                string.IsNullOrWhiteSpace(climate) ? "unknown" : climate,
                string.IsNullOrWhiteSpace(terrain) ? "unknown" : terrain,
                string.IsNullOrWhiteSpace(population) ? "unknown" : population
                );
        }

        // Regra de negócio de exemplo: um planeta é considerado "habitável conhecido"
        // se a população não é "unknown" e é diferente de "0".
        public bool IsPopulated() => Population != "unknown" && Population != "0";

    }
}
