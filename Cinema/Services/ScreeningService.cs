using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class ScreeningService : IScreeningService
{
    private readonly CinemaContext _context;
    private readonly DbSet<Screening> _screenings;

    public ScreeningService(CinemaContext context)
    {
        _context = context;
        _screenings = context.Screenings;
    }

    public IEnumerable<Screening> GetAll()
    {
        return _screenings;
    }

    public Screening? Get(int id)
    {
        return _screenings
            .FirstOrDefault(screening => screening.Id == id);
    }

    public Screening? Create(Screening address)
    {
        var addedScreening = _screenings.Add(address);
        _context.SaveChanges();
        return addedScreening.Entity;
    }
}