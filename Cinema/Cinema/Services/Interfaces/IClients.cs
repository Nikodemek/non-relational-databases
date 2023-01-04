using Cassandra;
using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IClients : ICommons<Client>
{
    Task<RowSet> ArchiveAsync(Guid id);
}