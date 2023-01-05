using Cinema.Models;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _orderService.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Order newTicket) => Ok(await _orderService.CreateAsync(newTicket));

    [HttpPost("Place/{clientId:guid}")]
    public async Task<IActionResult> Place([FromRoute] Guid clientId, [FromBody] Guid[] ticketIds) => Ok(await _orderService.PlaceAsync(clientId, ticketIds));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _orderService.DeleteAsync(id));
}