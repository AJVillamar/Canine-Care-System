using Domain.Shared.Utils;
using Domain.Shared.Exceptions;
using Domain.ModuleClient.Pets.Enums;
using System.Text.RegularExpressions;

namespace Domain.ModuleClient.Pets.Models
{
    public class Breed
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public SpeciesType Species { get; private set; }

        private Breed(Guid id, string name, SpeciesType specie)
        {
            Id = id;
            Name = StringUtils.Capitalize(name);
            Species = specie;
        }

        private Breed(Guid id)
        {
            Id = id;
            Name = "Unknown";
            Species = SpeciesType.Canine;
            Validate();
        }

        public static Breed Create(Guid? id, string name, string? specie = null)
        {
            var speciesParsed = SpeciesTypeExtensions.ParseFromSpanishName(specie ?? "Canino");

            return new Breed(
                id ?? Guid.NewGuid(),
                name,
                speciesParsed
            );
        }

        public static Breed Reference(Guid id)
        {
            var breed = new Breed(id);
            breed.ValidateIdOnly();
            return breed;
        }

        private void Validate()
        {
            if (Id == Guid.Empty)
                throw new EmptyFieldException("Id");

            if (string.IsNullOrWhiteSpace(Name))
                throw new EmptyFieldException("Nombre");

            if (Regex.IsMatch(Name, @"\d"))
                throw new BusinessRuleViolationException("El nombre de la raza no puede contener números.");
        }

        private void ValidateIdOnly()
        {
            if (Id == Guid.Empty)
                throw new EmptyFieldException("Id");
        }
    }
}
