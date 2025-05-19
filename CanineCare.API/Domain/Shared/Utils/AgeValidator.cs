using Domain.Shared.Exceptions;

namespace Domain.Shared.Utils
{
    public static class AgeValidator
    {
        public static void ValidateMinimumAge(DateTime birthDate, int minimumAge, string role)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (today < birthDate.AddYears(age))
                age--;

            if (age < minimumAge)
                throw new BusinessRuleViolationException($"El {role} debe tener al menos {minimumAge} años de edad.");
        }
    }
}
