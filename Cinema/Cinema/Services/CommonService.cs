using Cinema.Entity.Interfaces;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

public abstract class CommonService<TEntity> : ICommonService<TEntity> where TEntity : IMongoEntity<TEntity>
{
    private readonly ICommonsRepository<TEntity> _clientRepository;

    protected CommonService(ICommonsRepository<TEntity> clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return _clientRepository.GetAllAsync();
    }

    public Task<TEntity> GetAsync(string id)
    {
        return _clientRepository.GetAsync(id);
    }

    public Task CreateAsync(TEntity entity)
    {
        return _clientRepository.CreateAsync(entity);
    }

    public Task<DeleteResult> RemoveAsync(string id)
    {
        return _clientRepository.RemoveAsync(id);
    }
}