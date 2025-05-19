using MediatR;
using Shared.Responses;
using Application.ModuleIdentity.Authentication.Mappers;
using Domain.ModuleIdentity.Authentication.Repositories;
using Application.ModuleIdentity.Authentication.Validators;

namespace Application.ModuleIdentity.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResult<string>>
    {
        private readonly IAuthenticationRepository _repository;
        private readonly AuthenticationValidator _validator;
        private readonly AuthenticationMapperApp _mapper;
        
        public LoginCommandHandler(
            IAuthenticationRepository repository,
            AuthenticationValidator validator,
            AuthenticationMapperApp mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateLoginAsync(request);
            var credentials = _mapper.ToDomainCredentials(request);
            var token = await _repository.AuthenticateAsync(credentials);
            return new ApiResult<string>(200, token, "Inicio de sesión exitoso");
        }
    }
}
