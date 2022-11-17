using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ICommonService<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(ObjectId id);
    Task CreateAsync(T entity);
    Task<DeleteResult> RemoveAsync(ObjectId id);
}