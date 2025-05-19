using Application.ModuleAdministration.Professionals.Mappers;
using Application.ModuleAdministration.Professionals.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ModuleAdministration.Professionals.DependencyInjection
{
    public static class ProfessionalApplicationServiceCollectionExtensions
    {
        public static void AddProfessionalApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ProfessionalMapperApp>();
            services.AddScoped<ProfessionalValidator>();
        }
    }
}
