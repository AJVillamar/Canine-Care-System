using Domain.Shared.Exceptions;
using Domain.ModuleClient.Services.Enums;

namespace Domain.ModuleClient.Services.Models
{
    public class ServiceDetail
    {
        public Guid Id { get; private set; } 

        public string Name { get; private set; } 

        public string Description { get; private set; }

        public ServiceType Type { get; private set; }

        public List<ServiceActionDetail> Actions { get; private set; }

        private ServiceDetail(Guid id)
        {
            if (id == Guid.Empty)
                throw new EmptyFieldException("Id del servicio");
            
            Id = id;
        }

        private ServiceDetail(
            Guid id, string name, string description, ServiceType type,
            List<ServiceActionDetail> actions)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Actions = actions;

            Validate();
        }

        public static ServiceDetail Create(
                Guid? id, string name, string description, string type,
                List<ServiceActionDetail> actions)
        {
            return new ServiceDetail(
                id ?? Guid.NewGuid(),
                name,
                description,
                ServiceTypeExtensions.ParseFromSpanishName(type),
                actions
            );
        }

        public static ServiceDetail Create(Guid id)
        {
            return new ServiceDetail(id);
        }


        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new EmptyFieldException("nombre del servicio");

            if (string.IsNullOrWhiteSpace(Description))
                throw new EmptyFieldException("descripción del servicio");

            if (Actions == null || !Actions.Any())
                throw new DomainValidationException("Debe registrar al menos una acción.");
        }
    }
}
