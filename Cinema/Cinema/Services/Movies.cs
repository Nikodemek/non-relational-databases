using Cinema.Models;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class Movies : Commons<Movies, Movie>, IMovies
{ }