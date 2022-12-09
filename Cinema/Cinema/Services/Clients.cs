using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services;

public sealed class Clients : UniversalCommonsService<Client>, IClients
{
    public Clients(ILogger<Clients> logger)
        : base(logger, null)
    { }
    
    public async Task UpdateAsync(Client client)
    {
        await UpdateAsync(client.Id, client);
    }

    public async Task ArchiveAsync(string id)
    {
        await UpdateAsync(id, client => client.Archived = true);
    }
}