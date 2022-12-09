using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services;

public class MongoCommons<TCollection, TEntity> : MongoCollection<TCollection, TEntity>, ICommons<TEntity>
    where TCollection : MongoCollection<TCollection, TEntity>
    where TEntity : IEntity<TEntity>
{
    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return Collection
            .Find(_ => true)
            .ToListAsync();
    }

    public virtual Task<TEntity> GetAsync(string id)
    {
        return Collection
            .Find(e => e.Id == id)
            .SingleOrDefaultAsync();
    }

    public virtual Task CreateAsync(TEntity entity)
    {
        return Collection
            .InsertOneAsync(entity);
    }

    public virtual Task<DeleteResult> RemoveAsync(string id)
    {
        return Collection
            .DeleteOneAsync(e => e.Id == id);
    }
}