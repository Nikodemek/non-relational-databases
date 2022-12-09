using System.Threading.Tasks;
using Cinema.Extensions;
using Cinema.Models;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

namespace Cinema.Tests.RedisDataAccess;

public class CachedCommonsTest
{
    private static readonly Movie Movie1 = TestData.Movie1;
    private static readonly Movie Movie2 = TestData.Movie1 with
    {
        Title = "Incepcja", Length = 200
    };
    
    private IDistributedCache _cache;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _cache = new RedisCache(new RedisCacheOptions()
        {
            Configuration = "localhost:6379",
            InstanceName = "Cinema_",
        });
    }

    [Test, Order(0)]
    public async Task GetOne_Fail()
    {
        Movie? movie = await _cache.GetRecordAsync<Movie>(Movie1.Id);

        Assert.IsNull(movie);
    }

    [Test, Order(1)]
    public async Task InsertOne_Success()
    {
        await _cache.SetRecordAsync(Movie1.Id, Movie1);
    }

    [Test, Order(2)]
    public async Task GetOne_Success()
    {
        Movie? movie = await _cache.GetRecordAsync<Movie>(Movie1.Id);

        Assert.IsInstanceOf<Movie>(movie);
        
        movie.Id.Should().Be(Movie1.Id);
        movie.Length.Should().Be(Movie1.Length);
        movie.Title.Should().Be(Movie1.Title);
        movie.AgeCategory.Should().Be(Movie1.AgeCategory);
    }

    [Test, Order(3)]
    public async Task RemoveOne_Success()
    {
        await _cache.RemoveRecordAsync<Movie>(Movie1.Id);
        
        Movie? movie = await _cache.GetRecordAsync<Movie>(Movie1.Id);
        
        Assert.IsNull(movie);
    }
}