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
    private readonly ITicketService _ticketService;

    public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _ticketService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _ticketService.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Ticket newTicket) => Ok(await _ticketService.CreateAsync(newTicket));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _ticketService.DeleteAsync(id));
}