using Domain.Shared.Utils;
using Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ModuleIdentity.Roles.Models
{
    public class Role
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Role(string name)
            : this(Guid.NewGuid(), name) { }

        public Role(Guid id, string name)
        {
            Id = id;
            Name = StringUtils.Capitalize(name);

            Validate();
        }

        private void Validate()
        {
            if (Id == Guid.Empty)
                throw new EmptyFieldException("id");

            if (string.IsNullOrWhiteSpace(Name))
                throw new EmptyFieldException("Nombre");

            if (Regex.IsMatch(Name, "\\d"))
                throw new BusinessRuleViolationException("El nombre no puede contener números.");
        }
    }
}
