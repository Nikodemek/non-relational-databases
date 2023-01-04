using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Mappers;

public class ScreeningMapper : IEntityMapper<Screening, ScreeningDto>
{
    private readonly IMovieService _movieService;

    public ScreeningMapper(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<Screening> ToEntity(ScreeningDto dto)
    {
        return new Screening()
        {
            Id = dto.Id,
            Movie = await _movieService.GetAsync(dto.MovieId),
            Time = dto.Time,
        };
    }

    public Task<ScreeningDto> ToDto(Screening entity)
    {
        return Task.FromResult(new ScreeningDto()
        {
            Id = entity.Id,
            MovieId = entity.Movie?.Id ?? Guid.Empty,
            Time = entity.Time,
        });
    }
}