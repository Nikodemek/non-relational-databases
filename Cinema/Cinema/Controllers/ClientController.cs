using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Clients")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IClientService _clientService;

    public ClientController(ILogger<ClientController> logger, IClientService clientService)
    {
        _logger = logger;
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _clientService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _clientService.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Client newClient) => Ok(await _clientService.CreateAsync(newClient));

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Client updatedClient) => Ok(await _clientService.UpdateAsync(updatedClient.Id, updatedClient));

    [HttpPut("Archive/{id:guid}")]
    public async Task<IActionResult> Archive(Guid id) => Ok(await _clientService.ArchiveAsync(id));

    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _clientService.DeleteAsync(id));

}