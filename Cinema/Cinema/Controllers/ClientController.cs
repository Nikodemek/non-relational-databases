using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Clients")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IClients _clients;

    public ClientController(ILogger<ClientController> logger, IClients clients)
    {
        _logger = logger;
        _clients = clients;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _clients.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _clients.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Client newClient) => Ok(await _clients.CreateAsync(newClient));

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Client updatedClient) => Ok(await _clients.UpdateAsync(updatedClient.Id, updatedClient));

    [HttpPut("Archive/{id:guid}")]
    public async Task<IActionResult> Archive(Guid id) => Ok(await _clients.ArchiveAsync(id));

    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _clients.DeleteAsync(id));

}