using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class Screenings : CachedCommons<Screenings, Screening>, IScreenings
{
    public Screenings(IDistributedCache cache, MongoCommons<Screenings, Screening>? mongoCommons = default)
        : base(cache, mongoCommons)
    { }
}