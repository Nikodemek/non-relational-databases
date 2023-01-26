using Cinema.Entity;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class MovieService : CommonService<Movie>, IMovieService
{
    public MovieService(ICommonsRepository<Movie> clientRepository)
        : base(clientRepository)
    { }
}