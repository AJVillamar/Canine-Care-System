using Domain.Shared.Exceptions;
using Domain.Shared.ValueObjects;
using Domain.ModuleIdentity.Users.Models;

namespace Domain.ModuleClient.Owners.Models
{
    public class Owner : User
    {
        public Phone Phone { get; private set; }

        public string Address { get; private set; }

        private Owner(Guid id) : base(id)
        {
            if (id == Guid.Empty)
                throw new EmptyFieldException("Id del dueño");
        }

        private Owner(
            Guid id, Identification identification, Name firstName, 
            Name lastName, Email? email, Phone phone, string address )
            :base(id,identification, firstName, lastName, email )
        {
            Phone = phone;
            Address = address;

            Validate();
        }

        public static Owner Create(Guid id)
        {
            return new Owner(id);
        }

        public static Owner Create(
            Guid? id, string identification, string firstName,
            string lastName, string phone, string address, string? email = null)
        {
            var owner = new Owner(
                id ?? Guid.NewGuid(),
                Identification.Create(identification),
                Name.Create(firstName, "nombre"),
                Name.Create(lastName, "apellido"),
                string.IsNullOrWhiteSpace(email) ? null : Email.Create(email),
                Phone.Create(phone),
                address
            );

            if (id == null) owner.GeneratePassword();
            return owner;
        }

        public void UpdatePhone(string phone) => Phone = Phone.Create(phone);

        public void UpdateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new EmptyFieldException("Dirección");

            Address = address;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Address))
                throw new EmptyFieldException("Dirección");
        }
    }
}
