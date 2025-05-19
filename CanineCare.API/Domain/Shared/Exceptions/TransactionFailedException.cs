namespace Domain.Shared.Exceptions
{
    public class TransactionFailedException : DomainValidationException
    {
        public TransactionFailedException(string message) : base(message) { }
    }
}
