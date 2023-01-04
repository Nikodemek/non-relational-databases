using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;

namespace Cinema.Mappers;

public class MovieMapper : IEntityMapper<Movie, MovieDto>
{
    public Task<Movie> ToEntity(MovieDto dto)
    {
        return Task.FromResult(new Movie()
        {
            Id = dto.Id,
            Title = dto.Title,
            Length = dto.Length,
            AgeCategory = (AgeCategory)dto.AgeCategory,
        });
    }

    public Task<MovieDto> ToDto(Movie entity)
    {
        return Task.FromResult(new MovieDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Length = entity.Length,
            AgeCategory = (int)entity.AgeCategory,
        });
    }
}