using Cinema.Extensions;
using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public class RedisCommons<TEntity> : ICommons<TEntity>
    where TEntity : IEntity
{
    private static readonly TimeSpan RedisTimeoutMilliseconds = TimeSpan.FromMilliseconds(2000);
    
    private readonly IDistributedCache _cache;
    private (HashSet<string> Set, string Name) _ids;
    private readonly object _lock = new();
    
    public RedisCommons(IDistributedCache cache, string collectionName)
    {
        _cache = cache;
        _ids = (new HashSet<string>(), collectionName);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        await FetchIdsSet();
        if (_ids.Set.Count < 1) return new List<TEntity>();
        
        using CancellationTokenSource cancellationTokenSource = new(RedisTimeoutMilliseconds);
        TEntity?[] entities = await _cache.GetRecordsAsync<TEntity>(_ids.Set, cancellationTokenSource.Token);
            
        return new List<TEntity>(entities.Where(entity => entity is not null)!);
    }

    public async Task<List<TEntity>> GetAllWithIdsAsync(ICollection<string> ids)
    {
        using CancellationTokenSource cancellationTokenSource = new(RedisTimeoutMilliseconds);
        TEntity?[] entities = await _cache.GetRecordsAsync<TEntity>(ids, cancellationTokenSource.Token);
            
        return new List<TEntity>(entities.Where(entity => entity is not null)!);
    }

    public async Task<TEntity?> GetAsync(string id)
    {
        using CancellationTokenSource cancellationTokenSource = new(RedisTimeoutMilliseconds);
        return await _cache.GetRecordAsync<TEntity>(id, cancellationTokenSource.Token);
    }

    public async Task CreateAsync(TEntity entity)
    {
        using CancellationTokenSource cancellationTokenSource = new(RedisTimeoutMilliseconds);
        await _cache.SetRecordAsync<TEntity>(entity.Id, entity, token: cancellationTokenSource.Token);

        await FetchIdsSet();
        _ids.Set!.Add(entity.Id);
        
        await UpdateIdsSet();
    }

    public async Task UpdateAsync(string id, Action<TEntity> modExpr)
    {
        TEntity? entity = await GetAsync(id);
        if (entity is null) return;
        
        modExpr(entity);

        await UpdateAsync(id, entity);
    }

    public async Task UpdateAsync(string id, TEntity entity)
    {
        using CancellationTokenSource cancellationTokenSource = new(RedisTimeoutMilliseconds);

        entity.Id = id;
        await _cache.SetRecordAsync<TEntity>(entity.Id, entity, token: cancellationTokenSource.Token);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        using CancellationTokenSource cancellationTokenSource = new(RedisTimeoutMilliseconds);
        await _cache.RemoveRecordAsync<TEntity>(id, token: cancellationTokenSource.Token);

        await FetchIdsSet();
        bool success = _ids.Set!.Remove(id);
        
        if (success) await UpdateIdsSet();
        return success;
    }

    public async Task DeleteAllAsync()
    {
        await FetchIdsSet();
        if (_ids.Set.Count < 1) return;
        
        await _cache.RemoveRecordsAsync<TEntity>(_ids.Set);
        
        _ids.Set.Clear();
        await UpdateIdsSet();
    }

    public async Task CreateAllAsync(ICollection<TEntity> entities)
    {
        await FetchIdsSet();

        await _cache.SetRecordsAsync(entities.ToDictionary(e => e.Id));

        foreach (TEntity entity in entities)
        {
            _ids.Set.Add(entity.Id);
        }

        await UpdateIdsSet();
    }

    private async Task FetchIdsSet()
    {
        var collection = await _cache.GetRecordAsync<ICollection<string>>(_ids.Name);
        lock (_lock)
        {
            _ids.Set = collection is not null
                ? new HashSet<string>(collection)
                : new HashSet<string>();
        }
    }

    private async Task UpdateIdsSet()
    {
        string name;
        string[] setArray;

        lock (_lock)
        {
            name = _ids.Name;
            setArray = _ids.Set.ToArray();
        }
        
        using CancellationTokenSource cancellationTokenSource = new(RedisTimeoutMilliseconds);
        await _cache.SetRecordAsync<ICollection<string>>(name, setArray, token: cancellationTokenSource.Token);
    }
}