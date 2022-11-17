using Cinema.Models;
using Cinema.Services.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

public class Clients : Commons<Clients, Client>, IClients
{
    public Task<ReplaceOneResult> UpdateAsync(Client client)
    {
        return Collection
            .ReplaceOneAsync(c => c.Id == client.Id, client);
    }

    public Task<ReplaceOneResult> ArchiveAsync(int id)
    {
        var client = GetAsync(id).Result;
        return Collection
            .ReplaceOneAsync(c => c.Id == id, client with {Archived = true});
    }

    public Task RemoveAsync(int id)
    {
        return Collection
            .DeleteOneAsync(c => c.Id == id);
    }
}