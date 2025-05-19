namespace Domain.ModuleIdentity.Authentication.Models
{
    public class Credentials
    {
        public string Identification { get; private set; }

        public string Password { get; private set; }

        public Credentials(string identification, string password)
        {
            Identification = identification;
            Password = password;
        }
    }
}
