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
    public IEnumerable<Address> GetAll() => _addressService.GetAll();

    [HttpGet("{id:int}")]
    public Address? Get(int id) => _addressService.Get(id);

    [HttpPost("Register")]
    public Address? Register(Address newAddress) => _addressService.Create(newAddress);
}