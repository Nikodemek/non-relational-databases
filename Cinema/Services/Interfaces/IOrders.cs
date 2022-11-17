using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IOrders : ICommonService<Order>
{
    Task<Order> PlaceAsync(int clientId, int[] ticketIds);
}