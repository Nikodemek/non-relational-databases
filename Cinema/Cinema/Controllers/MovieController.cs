using Cinema.Entity;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Movies")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;

    public MovieController(ILogger<MovieController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _movieService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(await _movieService.GetAsync(id));

    [HttpPost]
    public async Task Register([FromBody] Movie newMovie) => await _movieService.CreateAsync(newMovie);
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id) => Ok(await _movieService.RemoveAsync(id));
}