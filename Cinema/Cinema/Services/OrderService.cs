using System.Text.Json;
using Cinema.Entity;
using Cinema.Kafka.Interfaces;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

internal sealed class OrderService : CommonService<Order>, IOrderService, IDisposable
{
    private readonly IClientsRepository _clientsRepository;
    private readonly ITicketsRepository _ticketsRepository;
    private readonly IKafkaProducer<Order> _kafkaProducer;

    public OrderService(
        IOrdersRepository ordersRepository,
        IClientsRepository clientsRepository,
        ITicketsRepository ticketsRepository,
        IKafkaProducer<Order> kafkaProducer)
        : base(ordersRepository)
    {
        _clientsRepository = clientsRepository;
        _ticketsRepository = ticketsRepository;
        _kafkaProducer = kafkaProducer;
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
        
        /*if (client.Archived || tickets.Any(t => t.Archived || t.Sold)) return order;

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
            .Select(t => _ticketsRepository.UpdateAsync(t.Id, t))
            .Append(CreateAsync(order))
            .Append(_clientsRepository.UpdateAsync(client.Id, client))
        );*/

        var result = await _kafkaProducer.ProduceAsync(order);
        Console.WriteLine($"Message successfully sent! Key: {result.Key}");

        return order;
    }

    public void Dispose()
    {
        _kafkaProducer.Dispose();
    }
}