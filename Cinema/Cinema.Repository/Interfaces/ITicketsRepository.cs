using Cinema.Entity;
using MongoDB.Driver;

namespace Cinema.Repository.Interfaces;

public interface ITicketsRepository : ICommonsRepository<Ticket>
{
    Task<IAsyncCursor<Ticket>> GetWithIdsAsync(ICollection<string> ids);
    Task<ReplaceOneResult> UpdateAsync(Ticket ticket);
    Task<ReplaceOneResult> ArchiveAsync(string id);
}