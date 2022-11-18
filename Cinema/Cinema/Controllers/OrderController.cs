using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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

    [HttpGet("{id}")]
    public async Task<Order> Get(string id) => await _orders.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Order newTicket) => await _orders.CreateAsync(newTicket);

    [HttpPost("Place/{clientId}")]
    public async Task<Order> Place([FromRoute] string clientId, [FromBody] string[] ticketIds) => await _orders.PlaceAsync(clientId, ticketIds);
    
    [HttpDelete("Remove/{id}")]
    public async Task Remove(string id) => await _orders.RemoveAsync(id);
}