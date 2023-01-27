using Cinema.Entity.Interfaces;
using Cinema.Repository.Interfaces;
using MongoDB.Driver;

namespace Cinema.Repository;

public abstract class CommonsRepository<TEntity> : MongoCollection<TEntity>, ICommonsRepository<TEntity>
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