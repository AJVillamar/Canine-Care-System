using MediatR;
using Shared.Responses;
using Domain.ModuleClient.Owners.Repositories;
using Application.ModuleClient.Owners.Mappers;
using Application.ModuleClient.Owners.Validators;

namespace Application.ModuleClient.Owners.Commands.UpdateOwner
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, ApiResult>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly OwnerValidator _validator;
        private readonly OwnerMapperApp _mapper;

        public UpdateOwnerCommandHandler(
            IOwnerRepository ownerRepository,
            OwnerValidator validator,
            OwnerMapperApp mapper)
        {
            _ownerRepository = ownerRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateUpdateAsync(request);
            var owner = await _ownerRepository.GetByIdAsync(request.Id);
            var ownerDomain = _mapper.ToDomain(request, owner!);
            await _ownerRepository.UpdateAsync(ownerDomain);
            return new ApiResult(200, "Dueño actualizada exitosamente.");
        }
    }
}
