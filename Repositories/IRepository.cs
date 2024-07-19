using HandsOnCQRS.Abstractions;

namespace HandsOnCQRS.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> AddAsync(T model);
    Task<bool> UpdateAsync(Guid id, T model);
    Task<bool> DeleteAsync(Guid id);
}
