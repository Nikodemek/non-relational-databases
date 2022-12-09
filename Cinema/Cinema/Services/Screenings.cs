using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class Screenings : UniversalCommonsService<Screening>, IScreenings
{
    public Screenings(ILogger<Screenings> logger, IDistributedCache cache)
        : base(logger, cache)
    { }
}