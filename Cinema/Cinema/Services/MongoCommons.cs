using Cinema.Data;
using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

public class MongoCommons<TEntity> : ICommons<TEntity>
    where TEntity : IEntity
{
    private IMongoCollection<TEntity> Collection => CinemaDb.Database.GetCollection<TEntity>(_collectionName);
    
    private readonly string _collectionName;
    
    public MongoCommons(string collectionName)
    {
        _collectionName = collectionName;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Collection
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<List<TEntity>> GetAllWithIdsAsync(ICollection<string> ids)
    {
        var results = await Collection.FindAsync(t => ids.Contains(t.Id));
        return results.ToList();
    }

    public async Task<TEntity?> GetAsync(string id)
    {
        return await Collection
            .Find(e => e.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, Action<TEntity> modExpr)
    {
        var entity = await GetAsync(id);
        if (entity is null) return;

        modExpr(entity);

        await UpdateAsync(id, entity);
    }

    public async Task UpdateAsync(string id, TEntity entity)
    {
        entity.Id = id;
        await Collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        DeleteResult result = await Collection.DeleteOneAsync(e => e.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}