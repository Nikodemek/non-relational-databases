using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface ITicketService : IGenericService<Ticket>
{
    IEnumerable<Ticket> GetWithIds(ICollection<int> ids);
    Ticket? Update(Ticket ticket);
    Ticket? Archive(int id);
}