using System;
using System.Threading.Tasks;
using Cinema.Mappers;
using Cinema.Models;
using Cinema.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Cinema.Tests.Services;

public class MovieServiceTests
{
    private static readonly Movie Movie1 = TestData.Movie1;
    private static readonly Movie Movie2 = TestData.Movie1 with 
        { Id = Guid.NewGuid(), Length = 15 };

    private MovieService _movieService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        DbManager.Initialize();
        
        _movieService = new (NullLogger<MovieService>.Instance, new MovieMapper());
    }
    
    [Test, Order(0)]
    public async Task InsertEntity_Success()
    {
        using var rowSet = await _movieService.CreateAsync(Movie1);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(1)]
    public async Task CountEntities_Success()
    {
        var allEntities = await _movieService.GetAllAsync();

        allEntities.Should().HaveCount(1);
        allEntities.Should().Contain(Movie1);
    }

    [Test, Order(2)]
    public async Task CheckMapping_Success()
    {
        var entity1 = await _movieService.GetAsync(Movie1.Id);
        
        entity1.Id.Should().Be(Movie1.Id);
        entity1.Title.Should().Be(Movie1.Title);
        entity1.Length.Should().Be(Movie1.Length);
        entity1.AgeCategory.Should().Be(Movie1.AgeCategory);
    }

    [Test, Order(3)]
    public async Task UpdateEntity_Success()
    {
        using var rowSet = await _movieService.UpdateAsync(Movie1.Id, Movie2 with { Id = Movie1.Id});

        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(4)]
    public async Task CheckUpdate_Success()
    {
        var entity2 = await _movieService.GetAsync(Movie1.Id);
        
        entity2.Id.Should().Be(Movie1.Id);
        entity2.Title.Should().Be(Movie2.Title);
        entity2.Length.Should().Be(Movie2.Length);
        entity2.AgeCategory.Should().Be(Movie2.AgeCategory);
    }

    [Test, Order(5)]
    public async Task InsertSecond_Success()
    {
        using var rowSet = await _movieService.CreateAsync(Movie2);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }
    
    [Test, Order(6)]
    public async Task GetWithMultipleIds_Success()
    {
        var entities = await _movieService.GetAllWithIdsAsync(new []
        {
            Movie1.Id,
            Movie2.Id
        });

        entities.Should().HaveCount(2);
    }

    [Test, Order(7)]
    public async Task DeleteOne_Success()
    {
        using var rowSet = await _movieService.DeleteAsync(Movie1.Id);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(8)]
    public async Task CheckDelete_Success()
    {
        var entities = await _movieService.GetAllAsync();

        entities.Should().HaveCount(1);
    }


    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _movieService.DeleteAsync(Movie1.Id);
        await _movieService.DeleteAsync(Movie2.Id);
    }
}