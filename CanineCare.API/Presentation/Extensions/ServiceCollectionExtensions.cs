using Infrastructure.DependencyInjection;
using Application.Shared.DependencyInjection;
using Application.ModuleClient.Pets.DependencyInjection;
using Application.ModuleClient.Owners.DependencyInjection;
using Application.ModuleClient.Breeds.DependencyInjection;
using Application.ModuleIdentity.Users.DependencyInjection;
using Application.ModuleAdministration.Admins.DependencyInjection;
using Application.ModuleIdentity.Authentication.DependencyInjection;
using Application.ModuleAdministration.Professionals.DependencyInjection;

namespace Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
        {
            InfrastructureServiceCollectionExtensions.AddInfrastructureServices(services, configuration);

            ApplicationMediatRServiceCollectionExtensions.AddApplicationMediatR(services);
            OwnerApplicationServiceCollectionExtensions.AddOwnerApplicationServices(services);
            AdminApplicationServiceCollectionExtensions.AddAdminApplicationServices(services);
            UserApplicationServiceCollectionExtensions.AddUserApplicationServices(services);
            ProfessionalApplicationServiceCollectionExtensions.AddProfessionalApplicationServices(services);
            AuthenticationApplicationServiceCollectionExtensions.AddAuthenticationApplicationServices(services);
            BreedApplicationServiceCollectionExtensions.AddBreedApplicationServices(services);
            PetApplicationServiceCollectionExtensions.AddPetApplicationServices(services);
        }
    }
}
