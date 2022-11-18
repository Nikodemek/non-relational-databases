using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
    public async Task<IEnumerable<Movie>> GetAll() => await _movies.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<Movie> Get(string id) => await _movies.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Movie newMovie) => await _movies.CreateAsync(newMovie);
    
    [HttpDelete("Remove/{id}")]
    public async Task Remove(string id) => await _movies.RemoveAsync(id);
}