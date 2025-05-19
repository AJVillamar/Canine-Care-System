using Domain.Shared.Utils;
using Domain.Shared.ValueObjects;
using Domain.ModuleIdentity.Users.Models;

namespace Domain.ModuleAdministration.Admins.Models
{
    public class Admin : User
    {
        public DateTime BirthDate { get; private set; }

        private Admin(
            Guid id, Identification identification, Name firstName,
            Name lastName, Email? email, DateTime birthDate )
            : base( id, identification, firstName, lastName, email )
        {
            Validate();
        }

        public static Admin Create(
            Guid? id = null, string? identification = null, string? firstName = null,
            string? lastName = null, string? email = null,   DateTime? birthDate = null )
        {
            var admin = new Admin(
                id ?? Guid.NewGuid(),
                Identification.Create(identification ?? "0950682401"),
                Name.Create(firstName ?? "Angelo", "Nombre"),
                Name.Create(lastName ?? "Villamar", "Apellido"),
                Email.Create(email ?? "villamar.angelo.94@outlook.com"),
                birthDate ?? new DateTime(1994, 09, 29)
            );

            if (id == null) admin!.GeneratePassword();
            return admin;
        }

        private void Validate()
        {
            AgeValidator.ValidateMinimumAge(BirthDate, 18, "Administrador");
        }
    }
}
