using Domain.Shared.Exceptions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Mappers.Pets;
using Domain.ModuleClient.Services.Enums;
using Domain.ModuleClient.Services.Models;
using Domain.ModuleClient.Services.Repositories;

namespace Infrastructure.Data.Repositories.Pets
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ServiceMaperInfra _mapper;

        public ServiceRepository(
            ApplicationDbContext context,
            ServiceMaperInfra maper )
        {
            _context = context;
            _mapper = maper;
        }

        public async Task AddAsync(ServiceDetail entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var serviceEntity = _mapper.ToEntity(entity);
                await _context.Services.AddAsync(serviceEntity);

                var details = entity.Actions?.Select(d => _mapper.ToEntityDetail(d.Title, d.Description, serviceEntity.Id)).ToList();
                await _context.ServiceDetails.AddRangeAsync(details);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("No se pudo completar la transacción para crear el servicio.");
            }
        }

        public async Task<ServiceDetail?> GetByIdAsync(Guid id)
        {
            return await _context.Services
                .Include(s => s.ServiceDetails)
                .Where(s => s.Id == id)
                .Select(s => _mapper.ToDomain(s))
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceDetail?> GetByNameAsync(string name)
        {
            return await _context.Services
                .Include(s => s.ServiceDetails)
                .Where(s => s.Name!.ToLower() == name.ToLower())
                .Select(s => _mapper.ToDomain(s))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ServiceDetail>> GetAllAsync()
        {
            return await _context.Services
                .Include(s => s.ServiceDetails)
                .OrderBy(s => s.Type)
                .ThenBy(s => s.Name)
                .Select(s => _mapper.ToDomain(s))
                .ToListAsync();
        }

        public async Task<List<string>> GetAllServiceTypes()
        {
            return await Task.FromResult(
                Enum.GetValues(typeof(ServiceType))
                    .Cast<ServiceType>()
                    .Select(t => t.GetSpanishValue())
                    .ToList());
        }

        public Task UpdateAsync(ServiceDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
