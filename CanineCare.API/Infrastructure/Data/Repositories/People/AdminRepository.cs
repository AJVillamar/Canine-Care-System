using Domain.Shared.Exceptions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Domain.ModuleIdentity.Roles.Enums;
using Infrastructure.Data.Mappers.People;
using Infrastructure.Identity.Abstractions;
using Infrastructure.Data.Entities.Identity;
using Domain.ModuleAdministration.Admins.Models;
using Domain.ModuleAdministration.Admins.Repositories;

namespace Infrastructure.Data.Repositories.People
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonMapperInfra _personMapper;
        private readonly AdminMapperInfra _adminMapper;
        private readonly ICryptoService _crypto;

        public AdminRepository(
            ApplicationDbContext context,
            PersonMapperInfra personMapper,
            AdminMapperInfra adminMapper,
            ICryptoService crypto )
        {
            _context = context;
            _personMapper = personMapper;
            _adminMapper = adminMapper;
            _crypto = crypto;
        }

        public async Task AddAsync(Admin entity)
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

                var adminEntity = _adminMapper.ToEntity(entity);
                await _context.Admins.AddAsync(adminEntity);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("No se pudo completar la transacción para crear un administrador.");
            }
        }

        public async Task<bool> ExistsAdminRoleAsync()
        {
            return await _context.Persons
               .Include(u => u.Roles)
               .AnyAsync(u => u.Roles.Any(r => r.Name == RoleType.Administrator.GetSpanishValue()));
        }
    }
}
