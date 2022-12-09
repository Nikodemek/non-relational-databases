using Cinema.Extensions;
using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Cinema.Services;

public abstract class UniversalCommonsService<TEntity> : ICommons<TEntity>, IInvalidatable, IDisposable
    where TEntity : class, IEntity
{
    private const string TimeoutLog = "Timeout of {service}.";

    private static readonly string InstanceGuid = $"{typeof(TEntity).Name}_{Guid.NewGuid()}";
    private static readonly string InstanceKey = $"{typeof(TEntity).Name}_key";

    private bool UseRedis => _cache is not null && _isRedisOnline;
    
    private readonly ILogger<UniversalCommonsService<TEntity>> _logger;
    private readonly IDistributedCache? _cache;
    private readonly MongoCommons<TEntity> _mongoCommons;
    private readonly RedisCommons<TEntity>? _redisCommons;
    private readonly object _lock = new();

    private bool _isRedisOnline = true;

    protected UniversalCommonsService(ILogger<UniversalCommonsService<TEntity>> logger, IDistributedCache? cache = null)
    {
        string collectionName = typeof(TEntity).Name + "Collection";
        
        _logger = logger;
        _cache = cache;
        _mongoCommons = new MongoCommons<TEntity>(collectionName);
        if (cache is not null)
        {
            _isRedisOnline = true;
            _redisCommons = new RedisCommons<TEntity>(cache, collectionName);
        
            bool isRegistered = ValidityService.RegisterInvalidatable(this);
            if (!isRegistered)
                throw new Exception("Failed to register Invalidatable!");

            RedisOnlineChecker();
        }
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        if (UseRedis)
        {
            try
            {
                return await _redisCommons!.GetAllAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(TimeoutLog, "Redis");
            }
        }

        try
        {
            return await _mongoCommons.GetAllAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(TimeoutLog, "Mongo");
        }
        
        return new List<TEntity>();
    }

    public async Task<List<TEntity>> GetAllWithIdsAsync(ICollection<string> ids)
    {
        if (UseRedis)
        {
            try
            {
                return await _redisCommons!.GetAllWithIdsAsync(ids);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(TimeoutLog, "Redis");
            }
        }

        try
        {
            return await _mongoCommons.GetAllWithIdsAsync(ids);
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(TimeoutLog, "Mongo");
        }
        
        return new List<TEntity>();
    }

    public async Task<TEntity?> GetAsync(string id)
    {
        if (UseRedis)
        {
            try
            {
                return await _redisCommons!.GetAsync(id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(TimeoutLog, "Redis");
            }
        }

        try
        {
            return await _mongoCommons.GetAsync(id);
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(TimeoutLog, "Mongo");
        }
        
        return null;
    }

    public async Task CreateAsync(TEntity entity)
    {
        if (UseRedis)
        {
            try
            {
                await _redisCommons!.CreateAsync(entity);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(TimeoutLog, "Redis");
            }
        }

        try
        {
            await _mongoCommons.CreateAsync(entity);
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(TimeoutLog, "Mongo");
        }
    }

    public async Task UpdateAsync(string id, Action<TEntity> modExpr)
    {
        if (UseRedis)
        {
            try
            {
                await _redisCommons!.UpdateAsync(id, modExpr);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(TimeoutLog, "Redis");
            }
        }

        try
        {
            await _mongoCommons.UpdateAsync(id, modExpr);
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(TimeoutLog, "Mongo");
        }
    }

    public async Task UpdateAsync(string id, TEntity entity)
    {
        if (UseRedis)
        {
            try
            {
                await _redisCommons!.UpdateAsync(id, entity);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(TimeoutLog, "Redis");
            }
        }

        try
        {
            await _mongoCommons.UpdateAsync(id, entity);
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(TimeoutLog, "Mongo");
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        bool deletionResult = true;
        
        if (UseRedis)
        {
            try
            {
                deletionResult &= await _redisCommons!.DeleteAsync(id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(TimeoutLog, "Redis");
            }
        }

        try
        {
            deletionResult &= await _mongoCommons.DeleteAsync(id);
        }
        catch (OperationCanceledException)
        {
            _logger.LogError(TimeoutLog, "Mongo");
        }

        return deletionResult;
    }

    public async Task DeleteAllAsync(bool fullReset = false)
    {
        if (!UseRedis) return;
        
        await _redisCommons!.DeleteAllAsync();

        if (fullReset) await _mongoCommons.DeleteAllAsync();
    }

    public async Task RestoreAllAsync()
    {
        if (!UseRedis) return;
        
        var allEntities = await _mongoCommons.GetAllAsync();
        await _redisCommons!.CreateAllAsync(allEntities);
    }
    
    private async void RedisOnlineChecker()
    {
        if (_cache is null) return;

        TimeSpan delay = TimeSpan.FromMilliseconds(2000);
        TimeSpan tokenDelay = delay / 5;
        bool instanceGuidSet = false;
        
        while (true)
        {
            using CancellationTokenSource tokenSource = new(tokenDelay);

            string? result = null;
            try
            {
                if (!instanceGuidSet)
                {
                    await _cache.SetStringAsync(InstanceKey, InstanceGuid, tokenSource.Token);
                    result = InstanceGuid;
                    instanceGuidSet = true;
                }
                else
                {
                    result = await _cache.GetStringAsync(InstanceKey, tokenSource.Token);
                }
            }
            catch (Exception e) when (e is OperationCanceledException or RedisTimeoutException)
            {
                _logger.LogWarning("Redis is offline!");
            }

            bool isOnline = result == InstanceGuid;
            lock (_lock)
            {
                _isRedisOnline = isOnline;
            }
            
            await Task.Delay(delay);
        }
    }
    
    private void RemoveInstanceGuid() => _cache?.Remove(InstanceKey);

    public void Dispose()
    {
        RemoveInstanceGuid();
    }
}