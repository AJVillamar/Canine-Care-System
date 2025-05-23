using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;
using Application.ModuleClient.Pets.Mappers;
using Application.ModuleClient.Pets.Validators;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, ApiResult<PetDto>>
    {
        private readonly IPetRepository _repository;
        private readonly PetValidator _validator;
        private readonly PetMapperApp _mapper;

        public GetPetByIdQueryHandler(
            IPetRepository repository, 
            PetValidator validator, 
            PetMapperApp mapper )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<PetDto>> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdAsync(request.Id);
            var pet = await _repository.GetByIdAsync(request.Id);
            var petDto = _mapper.ToDto(pet);
            return new ApiResult<PetDto>(200, petDto, "Información de la mascota.");
        }
    }
}
