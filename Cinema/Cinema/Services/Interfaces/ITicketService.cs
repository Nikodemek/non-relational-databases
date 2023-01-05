using Cassandra;
using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface ITicketService : ICommonService<Ticket>
{
    Task<RowSet> ArchiveAsync(Guid id);
}