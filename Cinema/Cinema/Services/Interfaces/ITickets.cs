using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface ITickets : ICommons<Ticket>
{
    Task UpdateAsync(Ticket ticket);
    Task ArchiveAsync(Guid id);
}