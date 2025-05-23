using Domain.ModuleIdentity.Users.Models;
using Domain.Shared.Exceptions;
using Domain.Shared.Utils;
using Domain.Shared.ValueObjects;

namespace Domain.ModuleAdministration.Professionals.Models
{
    public class Professional : User
    {
        public DateTime BirthDate { get; private set; }

        public int YearsOfExperience { get; private set; }

        private Professional(Guid id) : base(id)
        {
            if (id == Guid.Empty)
                throw new EmptyFieldException("Id del profesional");
        }

        private Professional(
            Guid id, Identification identification, Name firstName,
            Name lastName, Email? email, DateTime birthDate, int yearsOfExperience)
            : base(id, identification, firstName, lastName, email)
        {
            BirthDate = birthDate;
            YearsOfExperience = yearsOfExperience;
        
            Validate();
        }

        public static Professional Create(
            Guid? id, string identification, string firstName, string lastName,
            string email, DateTime birthDate, int yearsOfExperience)
        {
            var professional = new Professional(
                id ?? Guid.NewGuid(),
                Identification.Create(identification),
                Name.Create(firstName, "Nombre"),
                Name.Create(lastName, "Apellido"),
                Email.Create(email),
                birthDate,
                yearsOfExperience
            );

            if (id == null)
                professional.GeneratePassword();

            return professional;
        }

        public static Professional Create(Guid id)
        {
            return new Professional(id);
        }

        public void UpdateBirthDate(DateTime birthDate)
        {
            AgeValidator.ValidateMinimumAge(birthDate, 18, "Profesional");
            BirthDate = birthDate;
        }

        public void UpdateYearsOfExperience(int years)
        {
            if (years < 1)
                throw new BusinessRuleViolationException("El profesional debe tener al menos 1 año de experiencia.");

            YearsOfExperience = years;
        }

        private void Validate()
        {
            AgeValidator.ValidateMinimumAge(BirthDate, 18, "Profesional");

            if (YearsOfExperience < 1)
                throw new BusinessRuleViolationException("El profesional debe tener al menos 1 año de experiencia.");
        }
    }
}
