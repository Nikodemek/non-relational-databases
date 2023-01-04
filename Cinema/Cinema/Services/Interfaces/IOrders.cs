using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IOrders : ICommons<Order>
{
    Task<Order> PlaceAsync(Guid clientId, Guid[] ticketIds);
}