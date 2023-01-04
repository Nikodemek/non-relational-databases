using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Mappers;

public class OrderMapper : IEntityMapper<Order, OrderDto>
{
    private readonly IClients _clients;
    private readonly ITickets _tickets;

    public OrderMapper(IClients clients, ITickets tickets)
    {
        _clients = clients;
        _tickets = tickets;
    }

    public async Task<Order> ToEntity(OrderDto dto)
    {
        return new Order()
        {
            Id = dto.Id,
            Client = await _clients.GetAsync(dto.ClientId),
            PlacedTime = dto.PlacedTime,
            FinalPrice = dto.FinalPrice,
            Success = dto.Success,
            Tickets = await _tickets.GetAllWithIdsAsync(dto.TicketIds),
        };
    }

    public Task<OrderDto> ToDto(Order entity)
    {
        return Task.FromResult(new OrderDto()
        {
            Id = entity.Id,
            ClientId = entity.Client?.Id ?? Guid.Empty,
            PlacedTime = entity.PlacedTime,
            FinalPrice = entity.FinalPrice,
            Success = entity.Success,
            TicketIds = entity.Tickets
                .Select(x => x.Id)
                .ToArray()
        });
    }
}