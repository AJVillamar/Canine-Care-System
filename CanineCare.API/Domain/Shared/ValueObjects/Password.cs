using Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects
{
    public partial record Password
    {
        public string Value { get; }

        private const int MinLength = 8;

        private const int MaxLength = 15;

        private const string Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$";

        private Password(string value) => Value = value;

        public static Password Create(string value)
        {
            value = value.Trim();

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyFieldException("contraseña");

            if (value.Length < MinLength || value.Length > MaxLength)
                throw new BusinessRuleViolationException($"La contraseña debe tener entre {MinLength} y {MaxLength} caracteres.");

            if (!PasswordRegex().IsMatch(value))
                throw new BusinessRuleViolationException("La contraseña debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.");

            return new Password(value);
        }

        public static Password CreateFromIdentifier(string identifier)
        {
            var id = Identification.Create(identifier);
            return new Password(id.Value);
        }


        [GeneratedRegex(Pattern)]
        private static partial Regex PasswordRegex();
    }
}
