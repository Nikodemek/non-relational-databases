using Cinema.Entity;
using Cinema.Repository.Interfaces;

namespace Cinema.Repository;

public sealed class ScreeningsRepository : CommonsRepository<Screening>, IScreeningsRepository
{ }