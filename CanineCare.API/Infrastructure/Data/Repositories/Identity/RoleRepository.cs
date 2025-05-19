using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Domain.ModuleIdentity.Roles.Models;
using Infrastructure.Data.Mappers.Identity;
using Domain.ModuleIdentity.Roles.Repositories;

namespace Infrastructure.Data.Repositories.Identity
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleMapperInfra _mapper;

        public RoleRepository(
            ApplicationDbContext context,
            RoleMapperInfra mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddRangeAsync(IEnumerable<Role> entities)
        {
            var roles = entities.Select(_mapper.ToEntity).ToList();
            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }

        public async Task<Role?> GetByValueAsync(string value)
        {
            return await _context.Roles
                .Where(r => r.Name!.ToLower() == value.ToLower() && r.IsActive)
                .Select(r => _mapper.ToDomain(r))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .Select(r => _mapper.ToDomain(r))
                .ToListAsync();
        }

        public Task AddAsync(Role entitie)
        {
            throw new NotImplementedException();
        }

        public Task<Role?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
