using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class MovieService : IMovieService
{
    private readonly CinemaContext _context;
    private readonly DbSet<Movie> _movies;

    public MovieService(CinemaContext context)
    {
        _context = context;
        _movies = context.Movies;
    }

    public IEnumerable<Movie> GetAll()
    {
        return _movies;
    }

    public Movie? Get(int id)
    {
        return _movies
            .FirstOrDefault(movie => movie.Id == id);
    }

    public Movie? Create(Movie address)
    {
        var addedMovie = _movies.Add(address);
        _context.SaveChanges();
        return addedMovie.Entity;
    }
}