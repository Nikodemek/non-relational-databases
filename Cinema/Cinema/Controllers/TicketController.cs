using Cassandra;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Tickets")]
public class TicketController : ControllerBase
{
    private readonly ILogger<TicketController> _logger;
    private readonly ITickets _tickets;

    public TicketController(ILogger<TicketController> logger, ITickets tickets)
    {
        _logger = logger;
        _tickets = tickets;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _tickets.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _tickets.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Ticket newTicket) => Ok(await _tickets.CreateAsync(newTicket));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _tickets.DeleteAsync(id));
}