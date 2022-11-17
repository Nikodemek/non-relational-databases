using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Orders")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrders _orders;

    public OrderController(ILogger<OrderController> logger, IOrders orders)
    {
        _logger = logger;
        _orders = orders;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Order>> GetAll() => await _orders.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<Order> Get(int id) => await _orders.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register(Order newTicket) => await _orders.CreateAsync(newTicket);

    [HttpPost("Place/{clientId:int}")]
    public async Task<Order> Place(int clientId, [FromBody] int[] ticketIds) => await _orders.PlaceAsync(clientId, ticketIds);
}