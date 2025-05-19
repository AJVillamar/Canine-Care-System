using MediatR;
using Shared.Responses;
using Domain.Shared.Exceptions;
using Domain.ModuleIdentity.Roles.Enums;
using Domain.ModuleIdentity.Roles.Repositories;
using Application.ModuleAdministration.Professionals.Mappers;
using Domain.ModuleAdministration.Professionals.Repositories;
using Application.ModuleAdministration.Professionals.Validators;

namespace Application.ModuleAdministration.Professionals.Commands.CreateProfessional
{
    public class CreateProfessionalCommandHandler : IRequestHandler<CreateProfessionalCommand, ApiResult>
    {
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ProfessionalValidator _validator;
        private readonly ProfessionalMapperApp _mapper;

        public CreateProfessionalCommandHandler(
            IProfessionalRepository professionalRepository,
            IRoleRepository roleRepository,
            ProfessionalValidator validator,
            ProfessionalMapperApp mapper )
        {
            _professionalRepository = professionalRepository;
            _roleRepository = roleRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult> Handle(CreateProfessionalCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateCreateAsync(request);

            var roleType = RoleType.Professional;
            var role = await _roleRepository.GetByValueAsync(roleType.GetSpanishValue())
                ?? throw new NotFoundException($"Rol ({roleType.GetSpanishValue()})");

            var professional = _mapper.ToDomain(request);
            professional.AssignRole(role.Id);

            await _professionalRepository.AddAsync(professional);
            return new ApiResult(201, "Profesional creado exitosamente.");
        }
    }
}
