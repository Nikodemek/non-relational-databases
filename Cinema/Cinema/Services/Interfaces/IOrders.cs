using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IOrders : ICommons<Order>
{
    Task<Order> PlaceAsync(Guid clientId, ICollection<Guid> ticketIds);
}