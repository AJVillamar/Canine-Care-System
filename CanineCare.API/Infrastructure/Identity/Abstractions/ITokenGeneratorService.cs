namespace Infrastructure.Identity.Abstractions
{
    public interface ITokenGeneratorService
    {
        public string GenerateToken(Guid id, string names, string[] roles);
    }
}
