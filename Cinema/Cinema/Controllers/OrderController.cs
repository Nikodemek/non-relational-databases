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
    public async Task<IActionResult> GetAll() => Ok(await _orders.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _orders.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Order newTicket) => Ok(await _orders.CreateAsync(newTicket));

    [HttpPost("Place/{clientId:guid}")]
    public async Task<IActionResult> Place([FromRoute] Guid clientId, [FromBody] Guid[] ticketIds) => Ok(await _orders.PlaceAsync(clientId, ticketIds));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _orders.DeleteAsync(id));
}