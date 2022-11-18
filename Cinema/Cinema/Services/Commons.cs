using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services;

public abstract class Commons<TCollection, TEntity> : MongoCollection<TCollection, TEntity>, ICommonService<TEntity>
    where TCollection : MongoCollection<TCollection, TEntity>
    where TEntity : IMongoEntity<TEntity>
{
    public Task<List<TEntity>> GetAllAsync()
    {
        return Collection
            .Find(_ => true)
            .ToListAsync();
    }

    public Task<TEntity> GetAsync(string id)
    {
        return Collection
            .Find(e => e.Id == id)
            .SingleOrDefaultAsync();
    }

    public Task CreateAsync(TEntity entity)
    {
        return Collection
            .InsertOneAsync(entity);
    }

    public Task<DeleteResult> RemoveAsync(string id)
    {
        return Collection
            .DeleteOneAsync(e => e.Id == id);
    }
}