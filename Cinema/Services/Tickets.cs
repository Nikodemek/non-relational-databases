using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Cinema.Services;

public class Tickets : Commons<Tickets, Ticket>, ITickets
{
    public Task<IAsyncCursor<Ticket>> GetWithIds(ICollection<int> ids)
    {
        return Collection
            .FindAsync(t => ids.Contains(t.Id));
    }

    public Task<ReplaceOneResult> Update(Ticket ticket)
    {
        return Collection
            .ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
    }

    public Task<ReplaceOneResult> Archive(int id)
    {
        var ticket = GetAsync(id).Result;
        return Collection
            .ReplaceOneAsync(t => t.Id == id, ticket with {Archived = true});
    }
}