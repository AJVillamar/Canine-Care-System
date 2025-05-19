namespace Domain.Shared.Exceptions
{
    public class NotFoundException : DomainValidationException
    {
        public NotFoundException(string message) : base($"{message} no fue encontrado.") { }
    }
}
