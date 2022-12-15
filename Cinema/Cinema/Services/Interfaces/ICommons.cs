using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ICommons<T>
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllWithIdsAsync(ICollection<string> ids);
    Task<T?> GetAsync(string id);
    Task CreateAsync(T entity);
    Task UpdateAsync(string id, Action<T> modExpr);
    Task UpdateAsync(string id, T entity);
    Task<bool> DeleteAsync(string id);
}