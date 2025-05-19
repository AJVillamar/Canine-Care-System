using Domain.Shared.Exceptions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity.Abstractions;
using Domain.ModuleIdentity.Authentication.Models;
using Domain.ModuleIdentity.Authentication.Repositories;

namespace Infrastructure.Data.Repositories.Identity
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICryptoService _crypto;

        public AuthenticationRepository(
            ApplicationDbContext context,
            ICryptoService crypto )
        {
            _context = context;
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

            if (!_crypto.VerifyPassword(entity.Password, person.User.Password))
                throw new AuthenticationFailedException("Credenciales incorrectas.");

            var fullName = $"{person.FirstName} {person.LastName}";
            return fullName;
        }
    }
}