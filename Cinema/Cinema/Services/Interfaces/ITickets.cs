using Cinema.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ITickets : ICommons<Ticket>
{
    Task<IAsyncCursor<Ticket>> GetWithIdsAsync(ICollection<string> ids);
    Task<ReplaceOneResult> UpdateAsync(Ticket ticket);
    Task<ReplaceOneResult> ArchiveAsync(string id);
}