using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Mappers.Pets;
using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Pets.Repositories;

namespace Infrastructure.Data.Repositories.Pets
{
    public class BreedRepository : IBreedRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly BreedMapperInfra _mapper;

        public BreedRepository(
            ApplicationDbContext context,
            BreedMapperInfra mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Breed entity)
        {
            var breed = _mapper.ToEntity(entity);
            await _context.Breeds.AddAsync(breed);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Breed> entities)
        {
            var breeds = entities.Select(_mapper.ToEntity).ToList();
            await _context.Breeds.AddRangeAsync(breeds);
            await _context.SaveChangesAsync();
        }

        public async Task<Breed?> GetByIdAsync(Guid id)
        {
            return await _context.Breeds
                .Where(b => b.Id == id && b.IsActive)
                .Select(b => _mapper.ToDomain(b))
                .FirstOrDefaultAsync();
        }

        public async Task<Breed?> GetByValueAsync(string value)
        {
            return await _context.Breeds
                .Where(b => b.IsActive &&
                       (b.Name!.ToLower().Contains(value.ToLower()) ||
                        b.Species!.ToLower().Contains(value.ToLower())))
                .Select(b => _mapper.ToDomain(b))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Breed>> GetAllAsync()
        {
            return await _context.Breeds
                .Where(b => b.IsActive)
                .OrderBy(b => b.Name)
                .Select(b => _mapper.ToDomain(b))
                .ToListAsync();
        }
    }
}
