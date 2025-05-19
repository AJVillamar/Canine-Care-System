using Application.ModuleClient.Pets.Mappers;
using Application.ModuleClient.Pets.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ModuleClient.Pets.DependencyInjection
{
    public static class PetApplicationServiceCollectionExtensions
    {
        public static void AddPetApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<PetMapperApp>();
            services.AddScoped<PetExtraInfoMapperApp>();
            services.AddScoped<PetValidator>();
        }
    }
}
