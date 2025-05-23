using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Services.Models
{
    public class ServiceActionDetail
    {
        public string Title { get; private set; }

        public string Description { get; private set; }

        private ServiceActionDetail(string title, string description)
        {
            Title = title;
            Description = description;

            Validate();
        }

        public static ServiceActionDetail Create(string title, string description)
        {
            return new ServiceActionDetail(title, description);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new EmptyFieldException("título de la acción");

            if (string.IsNullOrWhiteSpace(Description))
                throw new EmptyFieldException("descripción de la acción");
        }
    }
}
