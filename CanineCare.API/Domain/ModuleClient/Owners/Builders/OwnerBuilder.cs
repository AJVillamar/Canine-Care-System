using Domain.ModuleClient.Owners.Models;

namespace Domain.ModuleClient.Owners.Builders
{
    public class OwnerBuilder
    {
        private Guid? _id;
        private string? _identification;
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private string? _phone;
        private string? _address;

        public OwnerBuilder() { }

        public OwnerBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public OwnerBuilder WithEmail(string email) => SetProperty(ref _email, email);
        public OwnerBuilder WithPhone(string phone) => SetProperty(ref _phone, phone);
        public OwnerBuilder WithAddress(string address) => SetProperty(ref _address, address);
        public OwnerBuilder WithLastName(string lastName) => SetProperty(ref _lastName, lastName);
        public OwnerBuilder WithFirstName(string firstName) => SetProperty(ref _firstName, firstName);
        public OwnerBuilder WithIdentification(string identification) => SetProperty(ref _identification, identification);

        public Owner Build()
        {
            return Owner.Create(
                _id,
                _identification,
                _firstName,
                _lastName,
                _phone,
                _address,
                _email
            );
        }

        public Owner BuildReference()
        {
            return Owner.Create(_id.Value);
        }

        private OwnerBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
