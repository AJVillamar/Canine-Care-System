using Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects
{
    public partial record Phone
    {
        public string Value { get; }

        private const int Length = 10;

        private const string Pattern = @"^09\d{8}$";

        private Phone(string value) => Value = value;

        public static Phone Create(string value)
        {
            value = value.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(value))
                throw new EmptyFieldException("Teléfono");

            if (!IsAllDigits(value))
                throw new BusinessRuleViolationException("El campo teléfono solo puede contener dígitos.");

            if (value.Length != Length)
                throw new BusinessRuleViolationException("El campo teléfono debe tener exactamente 10 dígitos.");

            if (!PhoneRegex().IsMatch(value))
                throw new BusinessRuleViolationException("El campo teléfono tiene un formato inválido.");

            return new Phone(value);
        }

        private static bool IsAllDigits(string value)
        {
            return value.All(char.IsDigit);
        }

        [GeneratedRegex(Pattern)]
        private static partial Regex PhoneRegex();
    }
}
