using Cassandra;
using Cassandra.Mapping;
using Cinema.Data;
using Cinema.Models.Interfaces;
using Cinema.Services.Interfaces;
using Cassandra.Data.Linq;
using Cinema.Mappers.Interfaces;
using Cinema.Models.Dto.Interfaces;

namespace Cinema.Services;

public abstract class CommonService<TEntity, TEntityDto> : ICommonService<TEntity>, IDisposable
    where TEntity : class, IEntity
    where TEntityDto: class, IEntityDto
{
    private readonly ILogger<CommonService<TEntity, TEntityDto>> _logger;

    protected readonly Table<TEntityDto> Table;
    protected readonly IEntityMapper<TEntity, TEntityDto> Mapper;

    protected CommonService(ILogger<CommonService<TEntity, TEntityDto>> logger, IEntityMapper<TEntity, TEntityDto> mapper)
    {
        _logger = logger;
        Mapper = mapper;
        Table = new Table<TEntityDto>(
            CinemaDb.Session,
            MappingConfiguration.Global, 
            typeof(TEntity).Name,
            CinemaDb.Keyspace);
        
        Table.CreateIfNotExists();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var result = await Table.ExecuteAsync();
        
        var list = new List<TEntity>();
        foreach (TEntityDto dto in result)
        {
            list.Add(await Mapper.ToEntity(dto));
        }
        return list;
    }

    public async Task<IEnumerable<TEntity>> GetAllWithIdsAsync(IEnumerable<Guid> ids)
    {
        var idsSet = ids.ToHashSet();
        
        var result = await Table
            .Where(x => idsSet.Contains(x.Id))
            .ExecuteAsync();
        
        var list = new List<TEntity>();
        foreach (TEntityDto dto in result)
        {
            list.Add(await Mapper.ToEntity(dto));
        }
        return list;
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        var result = await Table
            .FirstOrDefault(x => x.Id == id)
            .ExecuteAsync();

        if (result is null)
        {
            _logger.LogWarning("Entity of type '{Type}' with '{Field}' = '{Value}' was not found!", typeof(TEntity), nameof(id), id);
            throw new EntityNotFoundException(typeof(TEntity), nameof(id), id);
        }
        
        return await Mapper.ToEntity(result);
    }

    public async Task<RowSet> CreateAsync(TEntity entity)
    {
        var dto = await Mapper.ToDto(entity);
        
        return await Table
            .Insert(dto)
            .ExecuteAsync();
    }

    public async Task<RowSet> UpdateAsync(Guid id, Action<TEntity> modExpr)
    {
        var entity = await GetAsync(id);

        modExpr(entity);

        return await UpdateAsync(id, entity);
    }

    public async Task<RowSet> UpdateAsync(Guid id, TEntity entity)
    {
        entity.Id = id;
        
        var dto = await Mapper.ToDto(entity);
        
        return await Table
            .Where(x => x.Id == id)
            .Select(x => dto)
            .Update()
            .ExecuteAsync();
    }

    public Task<RowSet> DeleteAsync(Guid id)
    {
        return Table
            .Where(x => x.Id == id)
            .Delete()
            .ExecuteAsync();
    }

    public void Dispose()
    {
        
    }
}