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
    public IEnumerable<Client> GetAll() => _clientService.GetAll();

    [HttpGet("{id:int}")]
    public Client? Get(int id) => _clientService.Get(id);

    [HttpPost("Register")]
    public Client? Register(Client newClient) => _clientService.Create(newClient);

    [HttpPut("Update/{id:int}")]
    public Client? Update(Client updatedClient) => _clientService.Update(updatedClient);

    [HttpPut("Archive/{id:int}")]
    public Client? Archive(int id) => _clientService.Archive(id);

    [HttpDelete("Remove/{id:int}")]
    public bool Remove(int id) => _clientService.Remove(id);

}