using Cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Invalidate")]
public class InvalidationController : ControllerBase
{
    private readonly ILogger<InvalidationController> _logger;

    public InvalidationController(ILogger<InvalidationController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task InvalidateRedis() => await ValidityService.InvalidateAll();
}