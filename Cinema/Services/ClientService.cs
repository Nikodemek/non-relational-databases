using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class ClientService : IClientService
{
    private readonly CinemaContext _context;
    private readonly DbSet<Client> _clients;
    private readonly DbSet<Address> _addresses;

    public ClientService(CinemaContext context)
    {
        _context = context;
        _clients = context.Clients;
        _addresses = context.Addresses;
    }

    public IEnumerable<Client> GetAll()
    {
        return _clients
            .Include(c => c.Address);
    }

    public Client? Get(int id)
    {
        return _clients
            .Include(c => c.Address)
            .FirstOrDefault(c => c.Id == id);
    }

    public Client? Create(Client client)
    {
        var addedClient = _clients.Add(client);
        _context.SaveChanges();
        return addedClient.Entity;
    }

    public Client? Update(Client client)
    {
        var updatedClient = _clients.Update(client);
        _context.SaveChanges();
        return updatedClient.Entity;
    }

    public Client? Archive(int id)
    {
        var client = Get(id);
        if (client is null) return null;
        
        client.Archived = true;
        return Update(client);
    }

    public bool Remove(int id)
    {
        var client = Get(id);
        if (client is null) return false;
        
        _clients.Remove(client);
        _context.SaveChanges();
        return true;
    }
}