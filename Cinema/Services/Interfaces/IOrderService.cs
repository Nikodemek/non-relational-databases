using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IOrderService : IGenericService<Order>
{
    Order? Place(int clientId, int[] ticketIds);
}