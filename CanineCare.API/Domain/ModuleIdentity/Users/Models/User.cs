using Domain.Shared.Exceptions;
using Domain.Shared.ValueObjects;

namespace Domain.ModuleIdentity.Users.Models
{
    public class User
    {
        public Guid Id { get; private set; }

        public Identification Identification { get; private set; }

        public Name FirstName { get; private set; }

        public Name LastName { get; private set; }

        public Email? Email { get; private set; }

        public Password Password { get; private set; }

        public Guid RoleId { get; private set; }

        protected User(Guid id) { Id = id; }

        protected User(Guid id, Identification identification, Name firstName, Name lastName, Email? email)
        {
            Id = id;
            Identification = identification;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static User Create(Guid? id, string identification, string firstName, string lastName,string? email = null)
        {
            return new User(
                id ?? Guid.NewGuid(),
                Identification.Create(identification),
                Name.Create(firstName, "nombre"),
                Name.Create(lastName, "apellidos"),
                string.IsNullOrWhiteSpace(email) ? null : Email.Create(email)
            );
        }

        public void UpdateIdentification(string identification) => Identification = Identification.Create(identification);

        public void UpdateFirstName(string firstName) => FirstName = Name.Create(firstName, "nombres");

        public void UpdateLastName(string lastName) => LastName = Name.Create(lastName, "apellidos");

        public void UpdateEmail(string email) => Email = Email.Create(email);

        public void UpdatePassword(string password) => Password = Password.Create(password);

        public void AssignRole(Guid roleId)
        {
            if (roleId == Guid.Empty) throw new EmptyFieldException("rol");
            RoleId = roleId;
        }

        public void GeneratePassword() => Password = Password.CreateFromIdentifier(Identification.Value);
    }
}
