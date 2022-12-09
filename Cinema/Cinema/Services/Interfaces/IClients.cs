using Cinema.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface IClients : ICommons<Client>
{
    Task UpdateAsync(Client client);
    Task ArchiveAsync(string id);
}