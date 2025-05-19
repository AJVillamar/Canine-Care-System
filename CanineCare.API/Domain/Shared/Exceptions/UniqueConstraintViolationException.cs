namespace Domain.Shared.Exceptions
{
    public class UniqueConstraintViolationException : DomainValidationException
    {
        public UniqueConstraintViolationException(string message) : base($"El campo {message} tiene un valor ya registrado.") { }
    }
}
