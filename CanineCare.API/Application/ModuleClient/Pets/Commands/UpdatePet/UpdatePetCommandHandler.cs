using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Mappers;
using Domain.ModuleClient.Pets.Repositories;
using Application.ModuleClient.Pets.Validators;

namespace Application.ModuleClient.Pets.Commands.UpdatePet
{
    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, ApiResult>
    {
        private readonly IPetRepository _petRepository;
        private readonly PetValidator _validator;
        private readonly PetMapperApp _mapper;

        public UpdatePetCommandHandler(
            IPetRepository petRepository,
            PetValidator validator,
            PetMapperApp mapper)
        {
            _petRepository = petRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateUpdateAsync(request);
            var pet = await _petRepository.GetByIdAsync(request.Id);
            var updatedPet = _mapper.ToDomain(request, pet!);
            await _petRepository.UpdateAsync(updatedPet);
            return new ApiResult(200, "Mascota actualizada exitosamente.");
        }
    }
}
