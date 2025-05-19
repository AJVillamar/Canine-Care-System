namespace Infrastructure.Identity.Abstractions
{
    public interface ICryptoService
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string password, string hashedPassword);
    }
}
