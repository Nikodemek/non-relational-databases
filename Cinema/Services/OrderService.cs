using System.Collections.Immutable;
using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Cinema.Utils;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class OrderService : IOrderService
{
    private readonly CinemaContext _context;
    private readonly DbSet<Order> _orders;
    private readonly ITicketService _ticketService;
    private readonly IClientService _clientService;

    public OrderService(CinemaContext context, ITicketService ticketService, IClientService clientService)
    {
        _context = context;
        _ticketService = ticketService;
        _clientService = clientService;
        _orders = context.Orders;
    }

    public IEnumerable<Order> GetAll()
    {
        return _orders
            .Include(order => order.Client)
            .Include(order => order.Tickets);
    }

    public Order? Get(int id)
    {
        return _orders
            .Include(order => order.Client)
            .Include(order => order.Tickets)
            .FirstOrDefault(order => order.Id == id);
    }

    public Order? Create(Order address)
    {
        var addedOrder = _orders.Add(address);
        _context.SaveChanges();
        return addedOrder.Entity;
    }

    public Order? Place(int clientId, int[] ticketIds)
    {
        var tickets = _ticketService.GetWithIds(ticketIds).ToImmutableArray();
        var client = _clientService.Get(clientId);

        if (client is null) return null;

        var order = new Order()
        {
            Client = client,
            Tickets = tickets,
            Success = false
        };
        
        if (client.Archived || tickets.Any(t => t.Archived || t.Sold)) return order;

        int clientAge = Calculate.Age(client);
        if (tickets.Any(t => clientAge < (int) t.Screening.Movie.AgeCategory)) return order;

        decimal finalPrice = Calculate.FinalPrice(client, tickets);
        if (finalPrice > client.AccountBalance) return order;

        client.AccountBalance -= finalPrice;
        foreach (var ticket in tickets)
        {
            ticket.Sold = true;
        }

        order.Success = true;

        var createdOrder = Create(order);
        _clientService.Update(client);
        foreach (var ticket in tickets)
        {
            _ticketService.Update(ticket);
        }
        
        return createdOrder;
    }
}