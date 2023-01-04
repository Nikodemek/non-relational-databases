using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public abstract class UniversalCommonsService<TEntity> : ICommons<TEntity>, IDisposable
    where TEntity : class, IEntity
{
    private ILogger<UniversalCommonsService<TEntity>> log;
    
    protected UniversalCommonsService(ILogger<UniversalCommonsService<TEntity>> logger)
    {
        log = logger;
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetAllWithIdsAsync(ICollection<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, Action<TEntity> modExpr)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}