using HandsOnCQRS.Abstractions;

namespace HandsOnCQRS.Services;

public interface IService<T> where T : class, IDTO
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> AddAsync(T dto);
    Task<bool> UpdateAsync(Guid id, T dto);
    Task<bool> DeleteAsync(Guid id);
}
