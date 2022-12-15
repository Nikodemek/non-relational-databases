using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class Movies : UniversalCommonsService<Movie>, IMovies
{
    public Movies(ILogger<Movies> logger, IDistributedCache cache)
        : base(logger, cache)
    { }
}