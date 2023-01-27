using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ICommonService<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(string id);
    Task CreateAsync(T entity);
    Task<DeleteResult> RemoveAsync(string id);
}