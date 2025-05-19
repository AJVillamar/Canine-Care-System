using Domain.Shared.Utils;
using Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects
{
    public partial record Name
    {
        public string Value { get; }

        private const string Pattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+( [a-zA-ZáéíóúÁÉÍÓÚñÑ]+)?$";

        private Name(string value) => Value = value;

        public static Name Create(string value, string fieldType)
        {
            value = value.Trim();

            value = StringUtils.Capitalize(value);

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyFieldException(fieldType);

            if (Regex.IsMatch(value, @"\d"))
                throw new BusinessRuleViolationException($"El campo {fieldType} no puede contener números.");

            if (!NameRegex().IsMatch(value))
                throw new BusinessRuleViolationException($"El campo {fieldType} solo puede contener una o dos palabras.");
            return new Name(value);
        }

        [GeneratedRegex(Pattern)]
        private static partial Regex NameRegex();
    }
}
