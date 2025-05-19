namespace Domain.Shared.Exceptions
{
    public class EmptyFieldException : DomainValidationException
    {
        public EmptyFieldException(string message) : base($"El campo {message} es obligatorio.") { }
    }
}
