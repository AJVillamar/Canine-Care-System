using MediatR;
using Shared.Responses;
using Application.ModuleClient.Breeds.Mappers;
using Application.ModuleClient.Breeds.Validator;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Breeds.Commands.CreateBreed
{
    public class CreateBreedCommandHandler : IRequestHandler<CreateBreedCommand, ApiResult>
    {
        private readonly IBreedRepository _repository;
        private readonly BreedValidator _validator;
        private readonly BreedMapperApp _mapper;

        public CreateBreedCommandHandler(
            IBreedRepository repository,
            BreedValidator validator,
            BreedMapperApp mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult> Handle(CreateBreedCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateCreateAsync(request);
            var breed = _mapper.ToDomain(request);
            await _repository.AddAsync(breed);
            return new ApiResult(201, "Raza registrada exitosamente.");
        }
    }
}
