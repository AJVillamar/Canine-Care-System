using Microsoft.Extensions.DependencyInjection;
using Application.ModuleAdministration.Admins.Validators;

namespace Application.ModuleAdministration.Admins.DependencyInjection
{
    public static class AdminApplicationServiceCollectionExtensions
    {
        public static void AddAdminApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AdminValidator>();
        }
    }
}
