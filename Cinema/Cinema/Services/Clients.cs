﻿using Cinema.Models;
using Cinema.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services;

public sealed class Clients : MongoCommons<Clients, Client>, IClients
{
    public Task<ReplaceOneResult> UpdateAsync(Client client)
    {
        return Collection
            .ReplaceOneAsync(c => c.Id == client.Id, client);
    }

    public async Task<ReplaceOneResult> ArchiveAsync(string id)
    {
        var client = await GetAsync(id);
        return await Collection
            .ReplaceOneAsync(c => c.Id == id, client with {Archived = true});
    }
}