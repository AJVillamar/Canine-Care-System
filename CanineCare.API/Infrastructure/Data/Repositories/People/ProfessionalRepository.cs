using Domain.Shared.Exceptions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Mappers.People;
using Infrastructure.Identity.Abstractions;
using Infrastructure.Data.Entities.Identity;
using Domain.ModuleAdministration.Professionals.Models;
using Domain.ModuleAdministration.Professionals.Repositories;

namespace Infrastructure.Data.Repositories.People
{
    public class ProfessionalRepository : IProfessionalRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonMapperInfra _personMapper;
        private readonly ProfessionalMapperInfra _professionalMapper;
        private readonly ICryptoService _crypto;

        public ProfessionalRepository(
            ApplicationDbContext context,
            PersonMapperInfra personMapper,
            ProfessionalMapperInfra professionalMapper,
            ICryptoService crypto)
        {
            _context = context;
            _personMapper = personMapper;
            _professionalMapper = professionalMapper;
            _crypto = crypto;
        }

        public async Task AddAsync(Professional entity)
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

                var professionalEntity = _professionalMapper.ToEntity(entity);
                await _context.Professionals.AddAsync(professionalEntity);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("No se pudo completar la transacción para crear un profesional.");
            }
        }

        public async Task<IEnumerable<Professional>> GetAllAsync()
        {
            return await _context.Professionals
                .Include(t => t.Person)
                .Where(t => t.IsActive)
                .OrderBy(o => o.Person.LastName)
                .Select(t => _professionalMapper.ToDomain(t))
                .ToListAsync();
        }

        public Task AddRangeAsync(IEnumerable<Professional> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Professional?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Professional?> GetByValueAsync(string value)
        {
            throw new NotImplementedException();
        }
    }
}
