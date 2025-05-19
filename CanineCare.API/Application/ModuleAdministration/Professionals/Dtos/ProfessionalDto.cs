namespace Application.ModuleAdministration.Professionals.Dtos
{
    public class ProfessionalDto
    {
        public Guid Id { get; set; }

        public string? Identification { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int YearsOfExperience { get; set; }
    }
}
