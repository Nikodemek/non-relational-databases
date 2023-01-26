using Cinema.Entity;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class MovieService : CommonService<Movie>, IMovieService
{
    public MovieService(IMoviesRepository clientRepository)
        : base(clientRepository)
    { }
}