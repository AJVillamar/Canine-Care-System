using MediatR;
using Shared.Responses;
using Domain.Shared.Exceptions;
using Domain.ModuleIdentity.Roles.Enums;
using Domain.ModuleIdentity.Roles.Repositories;
using Domain.ModuleAdministration.Admins.Builders;
using Domain.ModuleAdministration.Admins.Repositories;
using Application.ModuleAdministration.Admins.Validators;

namespace Application.ModuleAdministration.Admins.Commands.CreatetDefaultAdmin
{
    public class CreateDefaultAdminCommandHandler : IRequestHandler<CreateDefaultAdminCommand, ApiResult>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly AdminValidator _validator;

        public CreateDefaultAdminCommandHandler(
            IAdminRepository adminRepository,
            IRoleRepository roleRepository,
            AdminValidator validator )
        {
            _adminRepository = adminRepository;
            _roleRepository = roleRepository;
            _validator = validator;
        }

        public async Task<ApiResult> Handle(CreateDefaultAdminCommand request, CancellationToken cancellationToken)
        {
            var roleType = RoleType.Administrator;

            var role = await _roleRepository.GetByValueAsync(roleType.GetSpanishValue())
                ?? throw new NotFoundException($"El Rol {roleType.GetSpanishValue()}");

            var admin = new AdminBuilder().Build();
            admin.AssignRole(role.Id);

            await _validator.ValidateCreateAsync(admin);
            await _adminRepository.AddAsync(admin);
            return new ApiResult(201, "Administrador creado exitosamente.");
        }
    }
}
