using Cassandra;

namespace Cinema.Services.Interfaces;

public interface ICommonService<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllWithIdsAsync(IEnumerable<Guid> ids);
    Task<T> GetAsync(Guid id);
    Task<RowSet> CreateAsync(T entity);
    Task<RowSet> UpdateAsync(Guid id, Action<T> modExpr);
    Task<RowSet> UpdateAsync(Guid id, T entity);
    Task<RowSet> DeleteAsync(Guid id);
}