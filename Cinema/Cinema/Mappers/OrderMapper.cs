using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Mappers;

public class OrderMapper : IEntityMapper<Order, OrderDto>
{
    private readonly IClientService _clientService;
    private readonly ITicketService _ticketService;

    public OrderMapper(IClientService clientService, ITicketService ticketService)
    {
        _clientService = clientService;
        _ticketService = ticketService;
    }

    public async Task<Order> ToEntity(OrderDto dto)
    {
        return new Order()
        {
            Id = dto.Id,
            Client = await _clientService.GetAsync(dto.ClientId),
            PlacedTime = dto.PlacedTime,
            FinalPrice = dto.FinalPrice,
            Success = dto.Success,
            Tickets = await _ticketService.GetAllWithIdsAsync(dto.TicketIds),
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