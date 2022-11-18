﻿using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services;

public class Tickets : Commons<Tickets, Ticket>, ITickets
{
    public Task<IAsyncCursor<Ticket>> GetWithIdsAsync(ICollection<ObjectId> ids)
    {
        return Collection
            .FindAsync(t => ids.Contains(t.Id));
    }

    public Task<ReplaceOneResult> UpdateAsync(Ticket ticket)
    {
        return Collection
            .ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
    }

    public async Task<ReplaceOneResult> ArchiveAsync(ObjectId id)
    {
        var ticket = await GetAsync(id);
        return await Collection
            .ReplaceOneAsync(t => t.Id == id, ticket with {Archived = true});
    }
}