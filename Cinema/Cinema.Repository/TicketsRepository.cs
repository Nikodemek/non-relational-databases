using Cinema.Entity;
using Cinema.Repository.Interfaces;
using MongoDB.Driver;

namespace Cinema.Repository;

public sealed class TicketsRepository : CommonsRepository<Ticket>, ITicketsRepository
{
    public Task<IAsyncCursor<Ticket>> GetWithIdsAsync(ICollection<string> ids)
    {
        return Collection
            .FindAsync(t => ids.Contains(t.Id));
    }

    public Task<ReplaceOneResult> UpdateAsync(Ticket ticket)
    {
        return Collection
            .ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
    }

    public async Task<ReplaceOneResult> ArchiveAsync(string id)
    {
        var ticket = await GetAsync(id);
        return await Collection
            .ReplaceOneAsync(t => t.Id == id, ticket with {Archived = true});
    }
}