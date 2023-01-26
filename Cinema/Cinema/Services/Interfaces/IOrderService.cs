using Cinema.Entity;

namespace Cinema.Services.Interfaces;

public interface IOrderService : ICommonService<Order>
{
    Task<Order> PlaceAsync(string clientId, string[] ticketIds);
}