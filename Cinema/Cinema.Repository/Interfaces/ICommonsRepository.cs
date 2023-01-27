using MongoDB.Driver;

namespace Cinema.Repository.Interfaces;

public interface ICommonsRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(string id);
    Task CreateAsync(T entity);
    Task<DeleteResult> RemoveAsync(string id);
}