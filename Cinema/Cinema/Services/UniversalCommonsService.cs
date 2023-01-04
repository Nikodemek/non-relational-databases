using Cassandra.Mapping;
using Cinema.Data;
using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public abstract class UniversalCommonsService<TEntity> : ICommons<TEntity>, IDisposable
    where TEntity : class, IEntity
{
    private ILogger<UniversalCommonsService<TEntity>> logger;

    protected IMapper Database;
    
    protected UniversalCommonsService(ILogger<UniversalCommonsService<TEntity>> logger)
    {
        this.logger = logger;
        Database = CinemaDb.Db;
        
        
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return Database.FetchAsync<TEntity>();
    }

    public Task<IEnumerable<TEntity>> GetAllWithIdsAsync(ICollection<Guid> ids)
    {
        return Database.FetchAsync<TEntity>();
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