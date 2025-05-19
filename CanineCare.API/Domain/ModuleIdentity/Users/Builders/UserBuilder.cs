using Domain.ModuleIdentity.Users.Models;

namespace Domain.ModuleIdentity.Users.Builders
{
    public class UserBuilder
    {
        private Guid? _id;
        private string? _identification;
        private string? _firstName;
        private string? _lastName;
        private string? _email;

        public UserBuilder () { }

        public UserBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public UserBuilder WithEmail(string email) => SetProperty(ref _email, email);
        public UserBuilder WithLastName(string lastName) => SetProperty(ref _lastName, lastName);
        public UserBuilder WithFirstName(string firstName) => SetProperty(ref _firstName, firstName);
        public UserBuilder WithIdentification(string identification) => SetProperty(ref _identification, identification);

        public User Build()
        {
            return User.Create(
                _id,
                _identification,
                _firstName,
                _lastName,
                _email
            );
        }

        private UserBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
