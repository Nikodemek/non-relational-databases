using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface ITicketService : IGenericService<Ticket>
{
    Ticket? Update(Ticket ticket);
    Ticket? Archive(int id);
}