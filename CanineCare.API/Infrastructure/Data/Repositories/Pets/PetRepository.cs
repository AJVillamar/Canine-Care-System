using Domain.Shared.Exceptions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Mappers.Pets;
using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Pets.Repositories;

namespace Infrastructure.Data.Repositories.Pets
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly PetMapperInfra _petMapper;
        private readonly PetExtraInfoMapperInfra _petExtraInfoMapper;

        public PetRepository(
            ApplicationDbContext context,
            PetMapperInfra petMapper,
            PetExtraInfoMapperInfra petExtraInfoMapper)
        {
            _context = context;
            _petMapper = petMapper;
            _petExtraInfoMapper = petExtraInfoMapper;
        }

        public async Task AddAsync(Pet entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var ownerEntity = await _context.Owners
                    .FirstOrDefaultAsync(o => (o.PersonId == entity.Owner.Id || o.Id == entity.Owner.Id) && o.IsActive);

                if (ownerEntity != null)
                {
                    var petEntity = _petMapper.ToEntity(entity);
                    petEntity.OwnerId = ownerEntity.Id;
                    await _context.Pets.AddAsync(petEntity);

                    var petExtraInfoEntity = _petExtraInfoMapper.ToEntity(entity.ExtraInfo);
                    petExtraInfoEntity.PetId = petEntity.Id;
                    await _context.PetExtras.AddAsync(petExtraInfoEntity);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("No se pudo completar la transacción para crear la mascota.");
            }
        }

        public async Task UpdateAsync(Pet entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingPet = await _context.Pets
                    .Include(p => p.Owner)
                        .ThenInclude(o => o.Person)
                    .Include(p => p.Breed)
                    .Include(p => p.PetExtraInfo)
                    .Where(p => p.Id == entity.Id && p.IsActive)
                    .FirstOrDefaultAsync();

                if (existingPet != null)
                {
                    var petEntity = _petMapper.ToEntity(existingPet, entity);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("No se pudo completar la transacción para actualizar la mascota.");
            }
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await _context.Pets
                .Include(p => p.Owner)
                    .ThenInclude(o => o.Person)
                .Include(p => p.Breed)
                .Include(p => p.PetExtraInfo)
                .Where(p => p.Id == id && p.IsActive)
                .Select(p => _petMapper.ToDomain(p))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pet>> GetByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Pets
                .Include(p => p.Owner)
                    .ThenInclude(o => o.Person)
                .Include(p => p.Breed)
                .Include(p => p.PetExtraInfo)
                .Where(p => (p.Owner.PersonId == ownerId || p.OwnerId == ownerId) && p.IsActive)
                .OrderBy(p => p.Name)
                .Select(p => _petMapper.ToDomain(p))
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetByOwnerIdentificationAsync(string identification)
        {
            return await _context.Pets
                .Include(p => p.Owner)
                    .ThenInclude(o => o.Person)
                .Include(p => p.Breed)
                .Include(p => p.PetExtraInfo)
                .Where(p => p.Owner.Person.Identification == identification && p.IsActive)
                .OrderBy(p => p.Name)
                .Select(p => _petMapper.ToDomain(p))
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> SearchByPetNameOrOwnerNameAsync(string searchTerm)
        {
            return await _context.Pets
                .Include(p => p.Owner)
                    .ThenInclude(o => o.Person)
                .Include(p => p.Breed)
                .Include(p => p.PetExtraInfo)
                .Where(p =>
                    p.IsActive &&
                    (
                        p.Name!.ToLower().Contains(searchTerm.ToLower()) ||
                        p.Owner.Person.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                        p.Owner.Person.LastName.ToLower().Contains(searchTerm.ToLower())
                    )
                )
                .OrderBy(p => p.Name)
                .Select(p => _petMapper.ToDomain(p))
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await _context.Pets
                .Include(p => p.Owner)
                    .ThenInclude(p => p.Person)
                .Include(p => p.Breed)
                .Include(p => p.PetExtraInfo)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .Select(p => _petMapper.ToDomain(p))
                .ToListAsync();
        }
    }
}
