using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects
{
    public class Identification
    {
        public string Value { get; }

        private Identification(string value) => Value = value;

        public static Identification Create(string value)
        {
            value = value.Trim();

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyFieldException("Identificación");

            return new Identification(value);
        }
    }
}
