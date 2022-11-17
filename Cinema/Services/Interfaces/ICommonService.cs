using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ICommonService<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    Task CreateAsync(T entity);
}