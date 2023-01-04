using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IOrderService : ICommonService<Order>
{
    Task<Order> PlaceAsync(Guid clientId, ICollection<Guid> ticketIds);
}