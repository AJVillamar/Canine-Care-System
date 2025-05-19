using Domain.Shared.Utils;
using Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ModuleCommon.Genders.Models
{
    public class Gender
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Gender(Guid id) : this(id, "Default") { }

        public Gender(string name) : this(Guid.NewGuid(), name) { }

        public Gender(Guid id, string name)
        {
            Id = id;
            Name = StringUtils.Capitalize(name);

            Validate();
        }

        private void Validate()
        {
            if (Id == Guid.Empty)
                throw new EmptyFieldException("Id");

            if (string.IsNullOrWhiteSpace(Name))
                throw new EmptyFieldException("Nombre");

            if (Regex.IsMatch(Name, "\\d"))
                throw new BusinessRuleViolationException("El nombre no puede contener números.");
        }
    }
}
