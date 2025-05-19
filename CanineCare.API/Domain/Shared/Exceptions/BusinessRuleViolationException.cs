namespace Domain.Shared.Exceptions
{
    public class BusinessRuleViolationException : DomainValidationException
    {
        public BusinessRuleViolationException(string message) : base(message) { }
    }
}
