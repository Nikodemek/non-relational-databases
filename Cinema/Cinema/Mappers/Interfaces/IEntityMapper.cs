using Cinema.Models.Dto.Interfaces;
using Cinema.Models.Interfaces;

namespace Cinema.Mappers.Interfaces;

public interface IEntityMapper<TEntity, TEntityDto>
    where TEntity : IEntity
    where TEntityDto : IEntityDto
{
    Task<TEntity> ToEntity(TEntityDto dto);
    Task<TEntityDto> ToDto(TEntity entity);
}