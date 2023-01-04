using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Movies")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovies _movies;

    public MovieController(ILogger<MovieController> logger, IMovies movies)
    {
        _logger = logger;
        _movies = movies;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _movies.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _movies.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Movie newMovie) => Ok(await _movies.CreateAsync(newMovie));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _movies.DeleteAsync(id));
}