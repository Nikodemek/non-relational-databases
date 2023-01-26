using Cinema.Entity.Interfaces;
using MongoDB.Driver;

namespace Cinema.Repository;

public abstract class MongoCollection<TEntity>
    where TEntity : IMongoEntity<TEntity>
{
    private static readonly string CollectionName = typeof(TEntity).Name;

    protected IMongoCollection<TEntity> Collection => CinemaConnection.Database.GetCollection<TEntity>(CollectionName);
}
