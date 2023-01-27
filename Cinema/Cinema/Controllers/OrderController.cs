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
    public async Task<IActionResult> GetAll() => Ok(await _orderService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(await _orderService.GetAsync(id));

    [HttpPost]
    public async Task Register([FromBody] Order newOrder) => await _orderService.CreateAsync(newOrder);

    [HttpPost("Place/{clientId}")]
    public async Task<IActionResult> Place([FromRoute] string clientId, [FromBody] string[] ticketIds) => Ok(await _orderService.PlaceAsync(clientId, ticketIds));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id) => Ok(await _orderService.RemoveAsync(id));
}