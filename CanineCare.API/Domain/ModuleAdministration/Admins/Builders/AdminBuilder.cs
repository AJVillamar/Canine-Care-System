using Domain.ModuleAdministration.Admins.Models;

namespace Domain.ModuleAdministration.Admins.Builders
{
    public class AdminBuilder
    {
        private Guid? _id;
        private string? _identification;
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private DateTime _birthDate;

        public AdminBuilder() { }

        public AdminBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public AdminBuilder WithEmail(string email) => SetProperty(ref _email, email);
        public AdminBuilder WithLastName(string lastName) => SetProperty(ref _lastName, lastName);
        public AdminBuilder WithPhone(DateTime birthDate) => SetProperty(ref _birthDate, birthDate);
        public AdminBuilder WithFirstName(string firstName) => SetProperty(ref _firstName, firstName);
        public AdminBuilder WithIdentification(string identification) => SetProperty(ref _identification, identification);

        public Admin Build()
        {
            return Admin.Create(
                _id,
                _identification,
                _firstName,
                _lastName,
                _email,
                _birthDate
            );
        }

        private AdminBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
