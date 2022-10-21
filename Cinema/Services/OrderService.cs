using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class OrderService : IOrderService
{
    private readonly CinemaContext _context;
    private readonly DbSet<Order> _movies;

    public OrderService(CinemaContext context)
    {
        _context = context;
        _movies = context.Orders;
    }

    public IEnumerable<Order> GetAll()
    {
        return _movies;
    }

    public Order? Get(int id)
    {
        return _movies
            .FirstOrDefault(order => order.Id == id);
    }

    public Order? Create(Order address)
    {
        var addedOrder = _movies.Add(address);
        _context.SaveChanges();
        return addedOrder.Entity;
    }
}