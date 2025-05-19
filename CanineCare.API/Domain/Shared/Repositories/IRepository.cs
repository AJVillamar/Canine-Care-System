namespace Domain.Shared.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);  

        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
