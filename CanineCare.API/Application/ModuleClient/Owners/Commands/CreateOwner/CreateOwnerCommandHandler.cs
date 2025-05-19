using MediatR;
using Shared.Responses;
using Domain.Shared.Exceptions;
using Domain.ModuleIdentity.Roles.Enums;
using Domain.ModuleClient.Owners.Repositories;
using Application.ModuleClient.Owners.Mappers;
using Domain.ModuleIdentity.Roles.Repositories;
using Application.ModuleClient.Owners.Validators;

namespace Application.ModuleClient.Owners.Commands.CreateOwner
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, ApiResult>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly OwnerValidator _validator;
        private readonly OwnerMapperApp _mapper;

        public CreateOwnerCommandHandler(
            IOwnerRepository ownerRepository,
            IRoleRepository roleRepository,
            OwnerValidator validator,
            OwnerMapperApp mapper)
        {
            _ownerRepository = ownerRepository;
            _roleRepository = roleRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateCreateAsync(request);

            var roleType = RoleType.Client;
            var role = await _roleRepository.GetByValueAsync(roleType.GetSpanishValue())
                ?? throw new NotFoundException($"Rol ({roleType.GetSpanishValue()})");

            var owner = _mapper.ToDomain(request);
            owner.AssignRole(role.Id);

            await _ownerRepository.AddAsync(owner);
            return new ApiResult(201, "Dueño creado exitosamente.");
        }
    }
}
