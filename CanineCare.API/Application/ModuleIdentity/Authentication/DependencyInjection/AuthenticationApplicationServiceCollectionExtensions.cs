using Microsoft.Extensions.DependencyInjection;
using Application.ModuleIdentity.Authentication.Mappers;
using Application.ModuleIdentity.Authentication.Validators;

namespace Application.ModuleIdentity.Authentication.DependencyInjection
{
    public static class AuthenticationApplicationServiceCollectionExtensions
    {
        public static void AddAuthenticationApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationMapperApp>();
            services.AddScoped<AuthenticationValidator>();
        }
    }
}
