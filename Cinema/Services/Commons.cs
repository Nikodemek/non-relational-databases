using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

public abstract class Commons<TCollection, TEntity> : MongoCollection<TCollection, TEntity>, ICommonService<TEntity>
    where TCollection : MongoCollection<TCollection, TEntity>
    where TEntity : IMongoEntity<TEntity>
{
    public async Task<List<TEntity>> GetAllAsync()
    {
        var cursor = await Collection
            .FindAsync(_ => true);
        return cursor.ToList();
    }

    public Task<TEntity> GetAsync(int id)
    {
        return Collection
            .Find(c => c.Id == id)
            .SingleOrDefaultAsync();
    }

    public Task CreateAsync(TEntity entity)
    {
        return Collection
            .InsertOneAsync(entity);
    }
}