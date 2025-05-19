using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Owners.Commands.UpdateOwner
{
    public class UpdateOwnerCommand : IRequest<ApiResult>
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}
