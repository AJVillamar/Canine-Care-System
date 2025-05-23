using Domain.ModuleClient.Services.Models;

namespace Domain.ModuleClient.Services.Builders
{
    public class ServiceDetailBuilder
    {
        private Guid? _id;
        private string _name;
        private string? _description;
        private string? _type;
        private List<ServiceActionDetail>? _actions;

        public ServiceDetailBuilder() { }

        public ServiceDetailBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public ServiceDetailBuilder WithName(string name) => SetProperty(ref _name, name);
        public ServiceDetailBuilder WithType(string type) => SetProperty(ref _type, type);
        public ServiceDetailBuilder WithDescription(string description) => SetProperty(ref _description, description);
        public ServiceDetailBuilder WithActions(List<ServiceActionDetail> actions) => SetProperty(ref _actions, actions);

        public ServiceDetail Build()
        {
            return ServiceDetail.Create(
                _id,
                _name,
                _description,
                _type,
                _actions
            );
        }

        public ServiceDetail BuildMinimal()
        {
            return ServiceDetail.Create(_id.Value);
        }


        private ServiceDetailBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
