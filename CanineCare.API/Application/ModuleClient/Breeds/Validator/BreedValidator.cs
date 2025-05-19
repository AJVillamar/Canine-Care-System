using Domain.Shared.Exceptions;
using Domain.ModuleClient.Pets.Repositories;
using Application.ModuleClient.Breeds.Commands.CreateBreed;

namespace Application.ModuleClient.Breeds.Validator
{
    public class BreedValidator
    {
        private readonly IBreedRepository _repository;

        public BreedValidator(IBreedRepository repository)
        {
            _repository = repository;
        }

        public async Task ValidateCreateAsync(CreateBreedCommand command)
        {
            if (await _repository.GetByValueAsync(command.Name) != null)
                throw new UniqueConstraintViolationException("Nombre");
        }
    }
}
