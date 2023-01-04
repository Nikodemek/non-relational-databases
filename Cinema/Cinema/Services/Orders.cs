using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;
using Cinema.Utils;

namespace Cinema.Services;

public sealed class Orders : UniversalCommonsService<Order, OrderDto>, IOrders
{
    private readonly ILogger<Orders> _logger;
    private readonly IClients _clients;
    private readonly ITickets _tickets;
    
    public Orders(ILogger<Orders> logger, IClients clients, ITickets tickets, IEntityMapper<Order, OrderDto> mapper)
        : base(logger, mapper)
    {
        _logger = logger;
        _clients = clients;
        _tickets = tickets;
    }

    public async Task<Order> PlaceAsync(Guid clientId, ICollection<Guid> ticketIds)
    {
        IEnumerable<Ticket> ticketsEnumerable = await _tickets.GetAllWithIdsAsync(ticketIds);
        Ticket[] tickets = ticketsEnumerable.ToArray();
        Client? client = await _clients.GetAsync(clientId);

        if (client is null)
            throw new Exception($"Client with id {clientId} does not exist!");

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

        await Task.WhenAll(tickets.Select(t => _tickets.UpdateAsync(t.Id, t))
            .Append(CreateAsync(order))
            .Append(_clients.UpdateAsync(client.Id, client))
        );
        
        return order;
    }
}