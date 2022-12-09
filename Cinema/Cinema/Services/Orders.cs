using Cinema.Models;
using Cinema.Services.Interfaces;
using Cinema.Utils;
using MongoDB.Driver;

namespace Cinema.Services;

public sealed class Orders : MongoCommons<Orders, Order>, IOrders
{
    private readonly IClients _clients;
    private readonly ITickets _tickets;
    
    public Orders(IClients clients, ITickets tickets)
    {
        _clients = clients;
        _tickets = tickets;
    }

    public async Task<Order> PlaceAsync(string clientId, string[] ticketIds)
    {
        var ticketsCursor = await _tickets.GetWithIdsAsync(ticketIds);
        var tickets = await ticketsCursor.ToListAsync();
        var client = await _clients.GetAsync(clientId);

        var order = new Order()
        {
            Client = client,
            Tickets = tickets,
            Success = false,
        };
        
        if (client.Archived || tickets.Any(t => t.Archived || t.Sold)) return order;

        int clientAge = Calculate.Age(client);
        if (tickets.Any(t => clientAge < (int) (t.Screening?.Movie?.AgeCategory ?? AgeCategory.G))) return order;

        decimal finalPrice = Calculate.FinalPrice(client, tickets);
        if (finalPrice > client.AccountBalance) return order;

        client.AccountBalance -= finalPrice;
        foreach (var ticket in tickets)
        {
            ticket.Sold = true;
        }

        order.Success = true;

        await Task.WhenAll(tickets
            .Select(t => _tickets.UpdateAsync(t))
            .Append(CreateAsync(order))
            .Append(_clients.UpdateAsync(client))
        );
        
        return order;
    }
}