using Domain.Shared.Exceptions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Domain.ModuleClient.Owners.Models;
using Infrastructure.Data.Mappers.People;
using Infrastructure.Identity.Abstractions;
using Infrastructure.Data.Entities.Identity;
using Domain.ModuleClient.Owners.Repositories;

namespace Infrastructure.Data.Repositories.People
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonMapperInfra _personMapper;
        private readonly OwnerMapperInfra _ownerMapper;
        private readonly ICryptoService _crypto;

        public OwnerRepository(
            ApplicationDbContext context,
            PersonMapperInfra personMapper,
            OwnerMapperInfra ownerMapper,
            ICryptoService crypto )
        {
            _context = context;
            _personMapper = personMapper;
            _ownerMapper = ownerMapper;
            _crypto = crypto;
        }

        public async Task AddAsync(Owner entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var personEntity = _personMapper.ToEntity(entity);
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == entity.RoleId);
                personEntity.Roles = new List<RoleEntity> { role! };
                await _context.Persons.AddAsync(personEntity);

                var userEntity = _personMapper.ToEntity(personEntity.Id, _crypto.HashPassword(entity.Password.Value));
                await _context.Users.AddAsync(userEntity);

                var ownerEntity = _ownerMapper.ToEntity(entity);
                await _context.Owners.AddAsync(ownerEntity);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("No se pudo completar la transacción para crear un dueño de la mascota.");
            }
        }

        public async Task UpdateAsync(Owner entity)
        {
            var existingOwner = await _context.Owners
                .Include(t => t.Person)
                .Where(t => (t.PersonId == entity.Id || t.Id == entity.Id) && t.IsActive)
                .FirstOrDefaultAsync();

            if (existingOwner != null)
            {
                _ownerMapper.ToEntity(existingOwner, entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Owner?> GetByIdAsync(Guid id)
        {
            return await _context.Owners
                .Include(o => o.Person)
                .Where(o => (o.Id == id || o.PersonId == id) && o.IsActive)
                .Select(o => _ownerMapper.ToDomain(o))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners
                .Include(o => o.Person)
                .Where(o => o.IsActive)
                .OrderBy(o => o.Person.LastName)
                .Select(o => _ownerMapper.ToDomain(o))
                .ToListAsync();
        }
    }
}
