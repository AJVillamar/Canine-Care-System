using Application.ModuleClient.Breeds.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Application.ModuleClient.Breeds.Validator;

namespace Application.ModuleClient.Breeds.DependencyInjection
{
    public static class BreedApplicationServiceCollectionExtensions
    {
        public static void AddBreedApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BreedMapperApp>();
            services.AddScoped<BreedValidator>();
        }
    }
}
