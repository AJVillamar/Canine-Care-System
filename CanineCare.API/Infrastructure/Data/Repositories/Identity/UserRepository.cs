using Infrastructure.Data.Context;
using Domain.ModuleIdentity.Users.Models;
using Infrastructure.Data.Mappers.People;
using Domain.ModuleIdentity.Users.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonMapperInfra _mapper;

        public UserRepository(
            ApplicationDbContext context,
            PersonMapperInfra user )
        {
            _context = context;
            _mapper = user;
        }

        public async Task<User?> GetByIdentificationAsync(string identification)
        {
            return await _context.Persons
                .Where(p => p.Identification == identification && p.IsActive)
                .Select(p => _mapper.ToDomain(p))
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Persons
                .Where(p => p.Email!.ToLower() == email.ToLower() && p.IsActive)
                .Select(p => _mapper.ToDomain(p))
                .FirstOrDefaultAsync();
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
