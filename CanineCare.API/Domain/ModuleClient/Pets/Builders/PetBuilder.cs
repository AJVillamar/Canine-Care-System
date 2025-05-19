using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Owners.Models;
using Domain.ModuleClient.Canines.Models;

namespace Domain.ModuleClient.Pets.Builders
{
    public class PetBuilder
    {
        private Guid? _id;
        private string? _name;
        private Breed? _breed;
        private DateTime _birthDate;
        private string? _sex;
        private string? _color;
        private double _weight;
        private PetExtraInfo? _extraInfo;
        private Owner? _owner;

        public PetBuilder() { }

        public PetBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public PetBuilder WithSex(string sex) => SetProperty(ref _sex, sex);
        public PetBuilder WithName(string name) => SetProperty(ref _name, name);
        public PetBuilder WithBreed(Breed breed) => SetProperty(ref _breed, breed);
        public PetBuilder WithOwner(Owner owner) => SetProperty(ref _owner, owner);
        public PetBuilder WithColor(string color) => SetProperty(ref _color, color);
        public PetBuilder WithWeight(double weight) => SetProperty(ref _weight, weight);
        public PetBuilder WithExtraInfo(PetExtraInfo extraInfo) => SetProperty(ref _extraInfo, extraInfo);
        public PetBuilder WithBirthDate(DateTime birthDate) => SetProperty(ref _birthDate, birthDate);

        public Pet Build()
        {
            return Pet.Create(
                _id,
                _name!,
                _breed!,
                _birthDate,
                _sex!,
                _color!,
                _weight,
                _extraInfo!,
                _owner!
            );
        }

        private PetBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
