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
    public IEnumerable<Order> GetAll() => _orderService.GetAll();

    [HttpGet("{id:int}")]
    public Order? Get(int id) => _orderService.Get(id);

    [HttpGet("Register")]
    public Order? Register(Order newTicket) => _orderService.Create(newTicket);
}