using MediatR;
using Shared.Responses;

namespace Application.ModuleIdentity.Authentication.Commands.Login
{
    public class LoginCommand : IRequest<ApiResult<string>>
    {
        public string? Identification { get; set; }

        public string? Password { get; set; }
    }
}
