using System;
using System.Collections.Generic;
using System.Text;

namespace SwapiExplorer.Domain.Entities
{
    // Entidade que representa um Personagem do universo Star Wars.
    public class Character
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string BirthYear { get; private set; }
        public string Gender { get; private set; }

        // Nome do planeta natal, resolvido pela Infrastructure (a SWAPI só entrega a URL do planeta)
        public string? HomeworldName { get; private set; }

        private Character(int id, string name, string birthYear, string gender, string? homeworldName)
        {
            Id = id;
            Name = name;
            BirthYear = birthYear;
            Gender = gender;
            HomeworldName = homeworldName;
        }

        public static Character Create(int id, string name, string birthYear, string gender, string? homeworldName = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("O nome do personagem é obrigatório.", nameof(name));
            }

            return new Character(id, name, birthYear, gender, homeworldName);
        }

        // Permite à camada de aplicação "enriquecer" o personagem depois de buscar o planeta natal.
        public void SetHomeworldName(string homeworldName) => HomeworldName = homeworldName;
    }
}
