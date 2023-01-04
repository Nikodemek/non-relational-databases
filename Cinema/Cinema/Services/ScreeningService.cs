using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class ScreeningService : CommonService<Screening, ScreeningDto>, IScreeningService
{
    public ScreeningService(ILogger<ScreeningService> logger, IEntityMapper<Screening, ScreeningDto> mapper)
        : base(logger, mapper)
    { }
}