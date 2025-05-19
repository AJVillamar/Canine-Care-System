using MediatR;
using Shared.Responses;
using Domain.Shared.Exceptions;
using Domain.ModuleIdentity.Roles.Repositories;

namespace Application.ModuleIdentity.Roles.Commands.CreateDefaultRoles
{
    public class CreateDefaultRolesCommandHandler : IRequestHandler<CreateDefaultRolesCommand, ApiResult>
    {
        private readonly IRoleRepository _repository;

        public CreateDefaultRolesCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult> Handle(CreateDefaultRolesCommand request, CancellationToken cancellationToken)
        {
            var defaultRoles = DefaultRolesProvider.GetDefaultRoles();

            var existingRoleNames = (await _repository.GetAllAsync())
                .Select(r => r.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var rolesToCreate = defaultRoles.Where(r => !existingRoleNames.Contains(r.Name, StringComparer.OrdinalIgnoreCase)).ToList();

            if (!rolesToCreate.Any())
                throw new BusinessRuleViolationException("Los roles predeterminados ya fueron creados.");

            await _repository.AddRangeAsync(rolesToCreate);
            var message = $"Proceso completado. Roles creados: {string.Join(", ", rolesToCreate.Select(r => r.Name))}.";
            return new ApiResult(201, message);
        }
    }
}
