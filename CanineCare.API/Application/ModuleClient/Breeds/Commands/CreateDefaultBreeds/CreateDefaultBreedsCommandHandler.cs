using MediatR;
using Shared.Responses;
using Domain.Shared.Exceptions;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Breeds.Commands.CreateDefaultBreeds
{
    public class CreateDefaultBreedsCommandHandler : IRequestHandler<CreateDefaultBreedsCommand, ApiResult>
    {
        private readonly IBreedRepository _repository;

        public CreateDefaultBreedsCommandHandler(IBreedRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult> Handle(CreateDefaultBreedsCommand request, CancellationToken cancellationToken)
        {
            var defaultBreeds = DefaultBreedsProvider.GetDefaultBreeds();

            var existingBreedNames = (await _repository.GetAllAsync())
                .Select(b => b.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var breedsToCreate = defaultBreeds.Where(b => !existingBreedNames.Contains(b.Name, StringComparer.OrdinalIgnoreCase));

            if (!breedsToCreate.Any())
                throw new BusinessRuleViolationException("Las razas predeterminados ya fueron creados.");

            await _repository.AddRangeAsync(breedsToCreate);
            var message = $"Proceso completado. Razas creadas: {string.Join(", ", breedsToCreate.Select(r => r.Name))}.";
            return new ApiResult(201, message);
        }
    }
}
