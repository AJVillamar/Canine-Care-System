using Domain.Shared.Exceptions;
using Domain.ModuleClient.Services.Repositories;

namespace Application.ModuleClient.Services.Validator
{
    public class ServiceValidator
    {
        private readonly IServiceRepository _repository;

        public ServiceValidator(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task ValidateCreateAsync(string name)
        {
            if (await _repository.GetByNameAsync(name) != null)
                throw new UniqueConstraintViolationException("Nombre");
        }

        public async Task ValidateGetByIdAsync(Guid id)
        {
            if (await _repository.GetByIdAsync(id) == null)
                throw new NotFoundException("El servicio");
        }
    }
}
