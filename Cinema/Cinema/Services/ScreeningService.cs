using Cinema.Entity;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class ScreeningService : CommonService<Screening>, IScreeningService
{
    public ScreeningService(IScreeningsRepository clientRepository)
        : base(clientRepository)
    { }
}