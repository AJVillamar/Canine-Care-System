using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Mappers;
using Domain.ModuleClient.Pets.Repositories;
using Application.ModuleClient.Pets.Validators;

namespace Application.ModuleClient.Pets.Commands.CreatePet
{
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, ApiResult>
    {
        private readonly IPetRepository _petRepository;
        private readonly PetValidator _validator;
        private readonly PetMapperApp _mapper;

        public CreatePetCommandHandler(
            IPetRepository petRepository,
            PetValidator validator,
            PetMapperApp mapper )
        {
            _petRepository = petRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateCreateAsync(request);
            var pet = _mapper.ToDomain(request);
            await _petRepository.AddAsync(pet);
            return new ApiResult(201, "Mascota registrada exitosamente.");
        }
    }
}
