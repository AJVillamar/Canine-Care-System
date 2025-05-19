namespace Domain.Shared.Repositories
{
    public interface ISimpleRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<T?> GetByValueAsync(string value);
    }
}
