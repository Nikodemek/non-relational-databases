using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Addresses")]
public class AddressController : ControllerBase
{
    private readonly ILogger<AddressController> _logger;
    private readonly IAddressService _addressService;

    public AddressController(ILogger<AddressController> logger, IAddressService addressService)
    {
        _logger = logger;
        _addressService = addressService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _addressService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _addressService.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Address newAddress) => Ok(await _addressService.CreateAsync(newAddress));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _addressService.DeleteAsync(id));
}