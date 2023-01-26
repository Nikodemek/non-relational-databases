using Cinema.Entity;
using Cinema.Entity.Enums;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;
using Cinema.Utils;
using MongoDB.Driver;

namespace Cinema.Services;

public sealed class OrderService : CommonService<Order>, IOrderService
{
    private readonly IClientsRepository _clientsRepository;
    private readonly ITicketsRepository _ticketsRepository;

    public OrderService(
        IOrdersRepository ordersRepository,
        IClientsRepository clientsRepository,
        ITicketsRepository ticketsRepository)
        : base(ordersRepository)
    {
        _clientsRepository = clientsRepository;
        _ticketsRepository = ticketsRepository;
    }

    public async Task<Order> PlaceAsync(string clientId, string[] ticketIds)
    {
        var ticketsCursor = await _ticketsRepository.GetWithIdsAsync(ticketIds);
        var tickets = await ticketsCursor.ToListAsync();
        var client = await _clientsRepository.GetAsync(clientId);

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
            .Select(t => _ticketsRepository.UpdateAsync(t))
            .Append(CreateAsync(order))
            .Append(_clientsRepository.UpdateAsync(client))
        );
        
        return order;
    }
}