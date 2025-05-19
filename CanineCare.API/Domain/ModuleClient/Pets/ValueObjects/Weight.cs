using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Pets.ValueObjects
{
    public class Weight
    {
        public double Value { get; }

        private Weight(double value) => Value = value;

        public static Weight Create(double value)
        {
            if (value <= 0)
                throw new BusinessRuleViolationException("El peso debe ser mayor a cero.");

            var truncatedValue = Math.Truncate(value * 100) / 100;

            return new Weight(truncatedValue);
        }
    }
}
