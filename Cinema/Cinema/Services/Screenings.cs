using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class Screenings : UniversalCommonsService<Screening, ScreeningDto>, IScreenings
{
    public Screenings(ILogger<Screenings> logger, IEntityMapper<Screening, ScreeningDto> mapper)
        : base(logger, mapper)
    { }
}