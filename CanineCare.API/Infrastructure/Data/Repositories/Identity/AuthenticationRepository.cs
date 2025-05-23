using Domain.Shared.Exceptions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Domain.ModuleIdentity.Roles.Enums;
using Infrastructure.Identity.Abstractions;
using Infrastructure.Data.Entities.Identity;
using Domain.ModuleIdentity.Authentication.Models;
using Domain.ModuleIdentity.Authentication.Repositories;

namespace Infrastructure.Data.Repositories.Identity
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenGeneratorService _token;
        private readonly ICryptoService _crypto;

        public AuthenticationRepository(
            ApplicationDbContext context,
            ITokenGeneratorService token,
            ICryptoService crypto )
        {
            _context = context;
            _token = token;
            _crypto = crypto;
        }

        public async Task<string> AuthenticateAsync(Credentials entity)
        {
            var person = await _context.Persons
                .Include(p => p.User)
                .Include(p => p.Roles)
                .FirstOrDefaultAsync(p => p.Identification!.ToLower() == entity.Identification.ToLower());

            if (person == null)
                throw new AuthenticationFailedException("Usuario no encontrado.");

            var user = person.User;

            if (user.LoginAttempts >= 3)
                throw new AuthenticationFailedException("Cuenta bloqueada por intentos fallidos.");

            if (!_crypto.VerifyPassword(entity.Password, user.Password))
            {
                user.LoginAttempts += 1;
                await _context.SaveChangesAsync();

                var remaining = Math.Max(0, 3 - user.LoginAttempts);
                var message = remaining > 0
                    ? $"Credenciales incorrectas. Intentos restantes: {remaining}."
                    : "Cuenta bloqueada por intentos fallidos.";

                throw new AuthenticationFailedException(message);
            }

            user.LoginAttempts = 0;
            await _context.SaveChangesAsync();

            if (!await HasActiveRoleAsync(person.Id, person!.Roles))
                throw new AuthenticationFailedException("No tienes un rol activo para acceder al sistema.");

            var fullName = $"{person!.FirstName} {person!.LastName}";
            var roles = person.Roles.Select(r => r.Name).ToArray();
            return _token.GenerateToken(person.Id, fullName, roles!);
        }

        private async Task<bool> HasActiveRoleAsync(Guid personId, ICollection<RoleEntity> roles)
        {
            var roleNames = roles.Select(r => r.Name).ToHashSet();

            if (roleNames.Contains(RoleType.Administrator.GetSpanishValue()))
                return await _context.Admins.AnyAsync(c => c.PersonId == personId && c.IsActive);

            if (roleNames.Contains(RoleType.Client.GetSpanishValue()))
                return await _context.Owners.AnyAsync(c => c.PersonId == personId && c.IsActive);

            if (roleNames.Contains(RoleType.Professional.GetSpanishValue()))
                return await _context.Professionals.AnyAsync(c => c.PersonId == personId && c.IsActive);

            return false;
        }
    }
}