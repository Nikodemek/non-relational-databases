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

    [HttpGet("{id:guid}")]
    public async Task<Order?> Get(Guid id) => await _orders.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Order newTicket) => await _orders.CreateAsync(newTicket);

    [HttpPost("Place/{clientId:guid}")]
    public async Task<Order> Place([FromRoute] Guid clientId, [FromBody] Guid[] ticketIds) => await _orders.PlaceAsync(clientId, ticketIds);
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task Remove(Guid id) => await _orders.DeleteAsync(id);
}