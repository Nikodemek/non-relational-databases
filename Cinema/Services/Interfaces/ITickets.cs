using Cinema.Models;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface ITickets : ICommonService<Ticket>
{
    Task<IAsyncCursor<Ticket>> GetWithIds(ICollection<int> ids);
    Task<ReplaceOneResult> Update(Ticket ticket);
    Task<ReplaceOneResult> Archive(int id);
}