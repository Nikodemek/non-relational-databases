using Cinema.Models;
using FluentAssertions;
using MongoDB.Driver;

namespace Cinema.Tests.DataAccess;

public class MovieTests
{
    private const string CollectionName = "Movies";
    
    private static readonly Movie Movie1 = TestData.Movie1;
    private static readonly Movie Movie2 = TestData.Movie1 with
    {
        Title = "Incepcja", Length = 200
    };
    
    private IMongoDatabase _db;
    private IMongoCollection<Movie> _movies;
    private int _id;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        (_id, _db) = DatabasesManager.GetDatabase();
        _movies = _db.GetCollection<Movie>(CollectionName);
    }
    
    [Test, Order(0)]
    public void InsertDocument_Success()
    {
        Assert.DoesNotThrow(() => _movies.InsertOne(Movie1));
    }
    
    [Test, Order(1)]
    public void CountCollections_Success()
    {
        var collections = _db.ListCollections().ToList();
        collections.Should().HaveCount(1);
    }
    
    [Test, Order(2)]
    public void GetAllDocuments_Success()
    {
        _movies.Find(_ => true).CountDocuments().Should().Be(1);
    }
    
    [Test, Order(3)]
    public void CheckDocument_Success()
    {
        var movie = _movies.Find(_ => true).First();

        movie.Title.Should().Be(Movie1.Title);
        movie.Length.Should().Be(Movie1.Length);
        movie.AgeCategory.Should().Be(Movie1.AgeCategory);
    }

    [Test, Order(4)]
    public void UpdateDocument_Success()
    {
        var movie = _movies.Find(_ => true).First();

        _movies.ReplaceOne(c => c.Id == movie.Id, Movie2 with {Id = movie.Id});
    }
    
    [Test, Order(5)]
    public void CheckDocumentAfterUpdate_Success()
    {
        var movie = _movies.Find(_ => true).First();

        movie.Title.Should().Be(Movie2.Title);
        movie.Length.Should().Be(Movie2.Length);
        movie.AgeCategory.Should().Be(Movie2.AgeCategory);
    }

    [Test, Order(6)]
    public void RemoveDocument_Success()
    {
        var movie = _movies.Find(_ => true).First();

        _movies.DeleteOne(c => c.Id == movie.Id);
    }
    
    [Test, Order(7)]
    public void GetAllDocumentsAfterDelete_Success()
    {
        _movies.Find(_ => true).CountDocuments().Should().Be(0);
    }
    
    [Test, Order(8)]
    public void DeleteCollection_Success()
    {
        _db.DropCollection(CollectionName);
    }
    
    [Test, Order(9)]
    public void CountCollectionsAfterDelete_Success()
    {
        var collections = _db.ListCollections().ToList();
        collections.Should().HaveCount(0);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        DatabasesManager.DeleteDatabase(_id);
    }
}