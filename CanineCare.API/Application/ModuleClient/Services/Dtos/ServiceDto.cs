namespace Application.ModuleClient.Services.Dtos
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; }

        public List<ServiceActionDto>? Actions { get; set; }
    }
}
