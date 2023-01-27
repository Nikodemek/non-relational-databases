using System.Threading.Tasks;
using Cinema.Entity;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(await _ticketService.GetAsync(id));

    [HttpPost]
    public async Task Register([FromBody] Ticket newTicket) => await _ticketService.CreateAsync(newTicket);

    [HttpPut]
    public async Task<IActionResult> Update(string id, [FromBody] Ticket updatedTicket) => Ok(await _ticketService.UpdateAsync(id, updatedTicket));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id) => Ok(await _ticketService.RemoveAsync(id));
}