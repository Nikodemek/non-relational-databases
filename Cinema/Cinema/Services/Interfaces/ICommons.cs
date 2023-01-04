namespace Cinema.Services.Interfaces;

public interface ICommons<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllWithIdsAsync(ICollection<Guid> ids);
    Task<T?> GetAsync(Guid id);
    Task CreateAsync(T entity);
    Task UpdateAsync(Guid id, Action<T> modExpr);
    Task UpdateAsync(Guid id, T entity);
    Task<bool> DeleteAsync(Guid id);
}