using Cinema.Entity;
using Cinema.Repository.Interfaces;

namespace Cinema.Repository;

public sealed class MoviesRepository : CommonsRepository<Movie>, IMoviesRepository
{ }