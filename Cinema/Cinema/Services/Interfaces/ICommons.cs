using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ICommons<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(string id);
    Task CreateAsync(T entity);
    Task<DeleteResult> RemoveAsync(string id);
}