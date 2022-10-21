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
        TestData.AddAllData(_context);
        return _tickets;
    }

    public Ticket? Get(int id)
    {
        return _tickets
            .Include(ticket => ticket.Screening)
            .FirstOrDefault(ticket => ticket.Id == id);
    }

    public IEnumerable<Ticket> GetWithIds(ICollection<int> ids)
    {
        return _tickets
            .Include(ticket => ticket.Screening)
            .Where(ticket => ids.Contains(ticket.Id));
    }

    public Ticket? Create(Ticket address)
    {
        var addedTicket = _tickets.Add(address);
        _context.SaveChanges();
        return addedTicket.Entity;
    }

    public Ticket? Update(Ticket ticket)
    {
        var updatedTicket = _tickets.Update(ticket);
        _context.SaveChanges();
        return updatedTicket.Entity;
    }

    public Ticket? Archive(int id)
    {
        var ticket = Get(id);
        if (ticket is null) return null;
        
        ticket.Archived = true;
        return Update(ticket);
    }
}