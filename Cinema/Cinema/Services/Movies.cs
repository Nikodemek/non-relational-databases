using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class Movies : UniversalCommonsService<Movie, MovieDto>, IMovies
{
    public Movies(ILogger<Movies> logger, IEntityMapper<Movie, MovieDto> mapper)
        : base(logger, mapper)
    { }
}