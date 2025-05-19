using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Domain.ModuleClient.Pets.Repositories;
using Domain.ModuleClient.Owners.Repositories;
using Domain.ModuleIdentity.Roles.Repositories;
using Domain.ModuleIdentity.Users.Repositories;
using Domain.ModuleIdentity.Authentication.Repositories;
using Domain.ModuleAdministration.Admins.Repositories;
using Domain.ModuleAdministration.Professionals.Repositories;

using Infrastructure.Data.Context;
using Infrastructure.Data.Mappers.People;
using Infrastructure.Data.Mappers.Identity;
using Infrastructure.Identity.Abstractions;
using Infrastructure.Data.Repositories.Pets;
using Infrastructure.Data.Repositories.People;
using Infrastructure.Identity.Implementations;
using Infrastructure.Data.Repositories.Identity;
using Infrastructure.Data.Mappers.Pets;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Conexion")));

            services.AddScoped<ICryptoService, CryptoService>();

            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();            
            services.AddScoped<IBreedRepository, BreedRepository>();
            services.AddScoped<IPetRepository, PetRepository>();

            services.AddScoped<RoleMapperInfra>();
            services.AddScoped<OwnerMapperInfra>();
            services.AddScoped<AdminMapperInfra>();
            services.AddScoped<ProfessionalMapperInfra>();
            services.AddScoped<PersonMapperInfra>();
            services.AddScoped<BreedMapperInfra>();
            services.AddScoped<PetMapperInfra>();
            services.AddScoped<PetExtraInfoMapperInfra>();
        }
    }
}
