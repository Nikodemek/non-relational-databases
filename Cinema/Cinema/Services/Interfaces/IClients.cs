using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IClients : ICommons<Client>
{
    Task UpdateAsync(Client client);
    Task ArchiveAsync(Guid id);
}