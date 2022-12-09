using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class Movies : CachedCommons<Movies, Movie>, IMovies
{
    public Movies(IDistributedCache cache, MongoCommons<Movies, Movie>? mongoCommons = default)
        : base(cache, mongoCommons)
    { }
}