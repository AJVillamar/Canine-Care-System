using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;
using Application.ModuleClient.Pets.Mappers;
using Application.ModuleClient.Owners.Validators;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Pets.Queries.GetPetsByOwnerId
{
    public class GetPetsByOwnerIdentificationQueryHandler : IRequestHandler<GetPetsByOwnerIdQuery, ApiResult<List<PetDto>>>
    {
        private readonly IPetRepository _repository;
        private readonly OwnerValidator _validator;
        private readonly PetMapperApp _mapper;

        public GetPetsByOwnerIdentificationQueryHandler(
            IPetRepository repository,
            OwnerValidator validator,
            PetMapperApp mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<PetDto>>> Handle(GetPetsByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdAsync(request.OwnerId);

            var pets = await _repository.GetByOwnerIdAsync(request.OwnerId);
            var petDtos = pets.Select(_mapper.ToDto).ToList();

            var message = petDtos.Any()
                ? $"Mascotas del dueño {request.OwnerId}"
                : "Este dueño no tiene mascotas registradas.";

            return new ApiResult<List<PetDto>>(200, petDtos, message);
        }
    }
}
