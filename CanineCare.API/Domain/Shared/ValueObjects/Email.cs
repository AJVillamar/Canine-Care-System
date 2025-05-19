using Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects
{
    public partial record Email
    {
        public string Value { get; }

        private const string Pattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";

        private Email(string value) => Value = value;

        public static Email Create(string value)
        {
            value = value.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(value))
                throw new EmptyFieldException("Correo Electrónico");

            if (!EmailRegex().IsMatch(value))
                throw new BusinessRuleViolationException("El campo correo electrónico tiene un formato inválido.");
            
            return new Email(value);
        }

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
