using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface ITickets : ICommons<Ticket>
{
    Task ArchiveAsync(Guid id);
}