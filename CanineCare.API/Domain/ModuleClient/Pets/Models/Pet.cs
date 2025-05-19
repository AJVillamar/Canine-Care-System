using Domain.Shared.Utils;
using Domain.Shared.Exceptions;
using Domain.ModuleClient.Pets.Enums;
using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Owners.Models;
using Domain.ModuleClient.Pets.ValueObjects;

namespace Domain.ModuleClient.Canines.Models
{
    public class Pet
    {
        public Guid Id { get; private set; }        

        public string Name { get; private set; }

        public Breed Breed { get; private set; }

        public DateTime BirthDate { get; private set; }

        public SexType Sex { get; private set; }

        public string Color { get; private set; }

        public Weight Weight { get; private set; }

        public PetExtraInfo ExtraInfo { get; private set; }

        public Owner Owner { get; private set; }

        private Pet(Guid id, string name, Breed breed, DateTime birthDate, SexType sex, string color, Weight weight, PetExtraInfo extraInfo, Owner owner)
        {
            Id = id;
            Name = StringUtils.Capitalize(name);
            Breed = breed;
            BirthDate = birthDate;
            Sex = sex;
            Color = color;
            Weight = weight;
            ExtraInfo = extraInfo;
            Owner = owner;

            Validate();
        }

        public static Pet Create(Guid? id, string name, Breed breed, DateTime birthDate, string sex, string color, double weight, PetExtraInfo extraInfo, Owner owner)
        {
            return new Pet(
                id ?? Guid.NewGuid(),
                name,
                breed,
                birthDate,
                SexTypeExtensions.ParseFromSpanishName(sex),
                color,
                Weight.Create(weight),
                extraInfo,
                owner
            );
        }

        public void UpdateName(string name)
        {
            Name = StringUtils.Capitalize(name);
            Validate();
        }
        
        public void UpdateBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
            Validate();
        }

        public void UpdateSex(string sex) => Sex = SexTypeExtensions.ParseFromSpanishName(sex);

        public void UpdateColor(string color)
        {
            Color = color;
            Validate();
        }
        public void UpdateBreed(Breed breed) => Breed = breed ?? throw new BusinessRuleViolationException("La raza no puede ser nula.");

        public void UpdateWeight(double weight) => Weight = Weight.Create(weight);

        public void UpdateExtraInfo(PetExtraInfo extraInfo) => ExtraInfo = extraInfo ?? throw new BusinessRuleViolationException("La información adicional no puede ser nula.");


        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new EmptyFieldException("Nombre de la mascota");

            if (Breed is null)
                throw new BusinessRuleViolationException("La raza es obligatoria.");

            if (BirthDate > DateTime.Now)
                throw new BusinessRuleViolationException("La fecha de nacimiento no puede ser futura.");

            if (string.IsNullOrWhiteSpace(Color))
                throw new EmptyFieldException("Color");

            if (Owner is null)
                throw new BusinessRuleViolationException("La mascota debe estar asociada a un dueño.");
        }
    }
}
