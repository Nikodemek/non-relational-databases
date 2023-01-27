using Cinema.Entity;
using Cinema.Repository.Interfaces;
using MongoDB.Driver;

namespace Cinema.Repository;

public sealed class OrdersRepository : CommonsRepository<Order>, IOrdersRepository
{ }