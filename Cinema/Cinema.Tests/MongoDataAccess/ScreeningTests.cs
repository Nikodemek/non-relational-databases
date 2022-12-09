using Cinema.Models;
using FluentAssertions;
using MongoDB.Driver;

namespace Cinema.Tests.MongoDataAccess;

public class TicketTests
{
    private const string CollectionName = "Tickets";
    
    private static readonly Ticket Ticket1 = TestData.Ticket1;
    private static readonly Ticket Ticket2 = TestData.Ticket1 with
    {
        Price = 50, Sold = true
    };
    
    private IMongoDatabase _db;
    private IMongoCollection<Ticket> _tickets;
    private int _id;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        (_id, _db) = DatabasesManager.GetDatabase();
        _tickets = _db.GetCollection<Ticket>(CollectionName);
    }
    
    [Test, Order(0)]
    public void InsertDocument_Success()
    {
        Assert.DoesNotThrow(() => _tickets.InsertOne(Ticket1));
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
        _tickets.Find(_ => true).CountDocuments().Should().Be(1);
    }
    
    [Test, Order(3)]
    public void CheckDocument_Success()
    {
        var ticket = _tickets.Find(_ => true).First();

        ticket.Price.Should().Be(Ticket1.Price);
        ticket.Sold.Should().Be(Ticket1.Sold);
        ticket.Archived.Should().Be(Ticket1.Archived);
    }

    [Test, Order(4)]
    public void UpdateDocument_Success()
    {
        var ticket = _tickets.Find(_ => true).First();

        _tickets.ReplaceOne(t => t.Id == ticket.Id, Ticket2 with {Id = ticket.Id});
    }
    
    [Test, Order(5)]
    public void CheckDocumentAfterUpdate_Success()
    {
        var ticket = _tickets.Find(_ => true).First();

        ticket.Price.Should().Be(Ticket2.Price);
        ticket.Sold.Should().Be(Ticket2.Sold);
        ticket.Archived.Should().Be(Ticket2.Archived);
    }

    [Test, Order(6)]
    public void RemoveDocument_Success()
    {
        var ticket = _tickets.Find(_ => true).First();

        _tickets.DeleteOne(t => t.Id == ticket.Id);
    }
    
    [Test, Order(7)]
    public void GetAllDocumentsAfterDelete_Success()
    {
        _tickets.Find(_ => true).CountDocuments().Should().Be(0);
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