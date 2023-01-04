using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;
using Cinema.Utils;

namespace Cinema.Services;

public sealed class OrderService : CommonService<Order, OrderDto>, IOrderService
{
    private readonly ILogger<OrderService> _logger;
    
    private readonly IClientService _clientService;
    private readonly ITicketService _ticketService;
    
    public OrderService(ILogger<OrderService> logger, IClientService clientService, ITicketService ticketService, IEntityMapper<Order, OrderDto> mapper)
        : base(logger, mapper)
    {
        _logger = logger;
        _clientService = clientService;
        _ticketService = ticketService;
    }

    public async Task<Order> PlaceAsync(Guid clientId, ICollection<Guid> ticketIds)
    {
        IEnumerable<Ticket> ticketsEnumerable = await _ticketService.GetAllWithIdsAsync(ticketIds);
        Ticket[] tickets = ticketsEnumerable.ToArray();
        Client? client = await _clientService.GetAsync(clientId);

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

        await Task.WhenAll(tickets.Select(t => _ticketService.UpdateAsync(t.Id, t))
            .Append(CreateAsync(order))
            .Append(_clientService.UpdateAsync(client.Id, client))
        );
        
        return order;
    }
}