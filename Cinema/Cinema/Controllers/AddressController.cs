using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Addresses")]
public class AddressController : ControllerBase
{
    private readonly ILogger<AddressController> _logger;
    private readonly IAddresses _addresses;

    public AddressController(ILogger<AddressController> logger, IAddresses addresses)
    {
        _logger = logger;
        _addresses = addresses;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Address>> GetAll() => await _addresses.GetAllAsync();

    [HttpGet("{id:guid}")]
    public async Task<Address?> Get(Guid id) => await _addresses.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Address newAddress) => await _addresses.CreateAsync(newAddress);
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task Remove(Guid id) => await _addresses.DeleteAsync(id);
}