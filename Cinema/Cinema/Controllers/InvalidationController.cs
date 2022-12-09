using Cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Invalidate")]
public class InvalidationController : ControllerBase
{
    private readonly ILogger<InvalidationController> _logger;
    private readonly IInvalidator _invalidator;

    public InvalidationController(ILogger<InvalidationController> logger, IInvalidator invalidator)
    {
        _logger = logger;
        _invalidator = invalidator;
    }

    [HttpGet]
    public async Task<bool> InvalidateRedis() => await _invalidator.InvalidateRedis();
}