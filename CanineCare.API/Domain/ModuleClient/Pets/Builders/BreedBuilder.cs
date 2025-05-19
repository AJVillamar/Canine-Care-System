using Domain.ModuleClient.Pets.Models;

namespace Domain.ModuleClient.Pets.Builders
{
    public class BreedBuilder
    {
        private Guid? _id;
        private string? _name;
        private string? _species;

        public BreedBuilder() { }

        public BreedBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public BreedBuilder WithName(string name) => SetProperty(ref _name, name);
        public BreedBuilder WithSpecies(string species) => SetProperty(ref _species, species);

        public Breed Build()
        {
            return Breed.Create(
                _id,
                _name,
                _species
            );
        }

        public Breed BuildReference()
        {
            return Breed.Reference(_id.Value);
        }

        private BreedBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
