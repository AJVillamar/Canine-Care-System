using Microsoft.Extensions.DependencyInjection;
using Application.ModuleIdentity.Users.Validators;

namespace Application.ModuleIdentity.Users.DependencyInjection
{
    public static class UserApplicationServiceCollectionExtensions
    {
        public static void AddUserApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<UserValidator>();
        }
    }
}
