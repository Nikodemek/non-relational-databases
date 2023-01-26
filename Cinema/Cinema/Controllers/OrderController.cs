using Cinema.Entity;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Orders")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Order>> GetAll() => await _orderService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<Order> Get(string id) => await _orderService.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Order newTicket) => await _orderService.CreateAsync(newTicket);

    [HttpPost("Place/{clientId}")]
    public async Task<Order> Place([FromRoute] string clientId, [FromBody] string[] ticketIds) => await _orderService.PlaceAsync(clientId, ticketIds);
    
    [HttpDelete("Remove/{id}")]
    public async Task Remove(string id) => await _orderService.RemoveAsync(id);
}