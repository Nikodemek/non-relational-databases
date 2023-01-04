using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class MovieService : CommonService<Movie, MovieDto>, IMovieService
{
    public MovieService(ILogger<MovieService> logger, IEntityMapper<Movie, MovieDto> mapper)
        : base(logger, mapper)
    { }
}