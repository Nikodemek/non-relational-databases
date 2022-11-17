using System.Transactions;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Cinema.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Services;

public class Orders : Commons<Orders, Order>, IOrders
{
    private readonly IClients _clients;
    private readonly ITickets _tickets;
    
    public Orders(IClients clients, ITickets tickets)
    {
        _clients = clients;
        _tickets = tickets;
    }

    public async Task<Order> PlaceAsync(ObjectId clientId, ObjectId[] ticketIds)
    {
        var ticketsCursor = await _tickets.GetWithIdsAsync(ticketIds);
        var tickets = ticketsCursor.ToList();
        var client = await _clients.GetAsync(clientId);

        var order = new Order()
        {
            Client = client,
            Tickets = tickets,
            Success = false
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

        using var scope = new TransactionScope();

        await CreateAsync(order);
        await _clients.UpdateAsync(client);
        
        foreach (var ticket in tickets)
        {
            await _tickets.UpdateAsync(ticket);
        }
        
        return order;
    }
}