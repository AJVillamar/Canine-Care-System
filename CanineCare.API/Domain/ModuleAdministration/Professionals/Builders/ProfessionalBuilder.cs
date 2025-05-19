using Domain.ModuleAdministration.Professionals.Models;

namespace Domain.ModuleAdministration.Professionals.Builders
{
    public class ProfessionalBuilder
    {
        private Guid? _id;
        private string? _identification;
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        public DateTime _birthDate;
        public int _yearsOfExperience;

        public ProfessionalBuilder() { }

        public ProfessionalBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public ProfessionalBuilder WithEmail(string email) => SetProperty(ref _email, email);
        public ProfessionalBuilder WithLastName(string lastName) => SetProperty(ref _lastName, lastName);
        public ProfessionalBuilder WithFirstName(string firstName) => SetProperty(ref _firstName, firstName);
        public ProfessionalBuilder WithBirthDate(DateTime birthDate) => SetProperty(ref _birthDate, birthDate);
        public ProfessionalBuilder WithIdentification(string identification) => SetProperty(ref _identification, identification);
        public ProfessionalBuilder WithYearsOfExperience(int yearsOfExperience) => SetProperty(ref _yearsOfExperience, yearsOfExperience);

        public Professional Build()
        {
            return Professional.Create(
                _id,
                _identification,
                _firstName,
                _lastName,
                _email,
                _birthDate,
                _yearsOfExperience
            );
        }

        private ProfessionalBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
