using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class TicketService : ITicketService
{
    private readonly CinemaContext _context;
    private readonly DbSet<Ticket> _tickets;

    public TicketService(CinemaContext context)
    {
        _context = context;
        _tickets = context.Tickets;
    }

    public IEnumerable<Ticket> GetAll()
    {
        return _tickets;
    }

    public Ticket? Get(int id)
    {
        return _tickets
            .FirstOrDefault(ticket => ticket.Id == id);
    }

    public Ticket? Create(Ticket address)
    {
        var addedTicket = _tickets.Add(address);
        _context.SaveChanges();
        return addedTicket.Entity;
    }
}