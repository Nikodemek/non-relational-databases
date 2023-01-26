using Cinema.Entity;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ITicketService : ICommonService<Ticket>
{
    Task<IAsyncCursor<Ticket>> GetWithIdsAsync(ICollection<string> ids);
    Task<ReplaceOneResult> UpdateAsync(Ticket ticket);
    Task<ReplaceOneResult> ArchiveAsync(string id);
}