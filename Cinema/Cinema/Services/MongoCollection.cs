using Cinema.Data;
using Cinema.Models.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

public abstract class MongoCollection<TCollection, TEntity>
    where TCollection : MongoCollection<TCollection, TEntity>
    where TEntity : IMongoEntity<TEntity>
{
    private static readonly string CollectionName = typeof(TCollection).Name;

    protected IMongoCollection<TEntity> Collection => CinemaDb.Database.GetCollection<TEntity>(CollectionName);
}
