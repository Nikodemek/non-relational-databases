using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IClientService : IGenericService<Client>
{
    Client? Update(Client client);
    Client? Archive(int id);
    bool Remove(int id);
}