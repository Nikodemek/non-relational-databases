using Cinema.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface IClients : ICommonService<Client>
{
    Task<ReplaceOneResult> UpdateAsync(Client client);
    Task<ReplaceOneResult> ArchiveAsync(string id);
}