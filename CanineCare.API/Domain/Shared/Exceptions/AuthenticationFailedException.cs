namespace Domain.Shared.Exceptions
{
    public class AuthenticationFailedException : DomainValidationException
    {
        public AuthenticationFailedException(string message) : base(message) { }
    }
}
