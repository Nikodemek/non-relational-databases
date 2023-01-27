using Cinema.Entity;
using Cinema.Repository.Interfaces;
using MongoDB.Driver;

namespace Cinema.Repository;

public sealed class ClientsRepository : CommonsRepository<Client>, IClientsRepository
{
    public Task<ReplaceOneResult> UpdateAsync(string id, Client client)
    {
        client.Id = id;
        
        return Collection
            .ReplaceOneAsync(c => c.Id == id, client);
    }

    public async Task<ReplaceOneResult> ArchiveAsync(string id)
    {
        var client = await GetAsync(id);
        return await Collection
            .ReplaceOneAsync(c => c.Id == id, client with {Archived = true});
    }
}