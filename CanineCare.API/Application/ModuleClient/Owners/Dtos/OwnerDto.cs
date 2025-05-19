namespace Application.ModuleClient.Owners.Dtos
{
    public class OwnerDto
    {
        public Guid Id { get; set; }

        public string? Identification { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}
