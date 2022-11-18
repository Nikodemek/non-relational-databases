using Cinema.Models;
using MongoDB.Bson;

namespace Cinema.Services.Interfaces;

public interface IOrders : ICommonService<Order>
{
    Task<Order> PlaceAsync(string clientId, string[] ticketIds);
}