using Cinema.Models;
using FluentAssertions;
using MongoDB.Driver;

namespace Cinema.Tests.MongoDataAccess;

public class ClientTests
{
    private const string CollectionName = "Clients";
    
    private static readonly Client Client1 = TestData.Client1;
    private static readonly Client Client2 = TestData.Client1 with
    {
        FirstName = "Marek", AccountBalance = 100, ClientType = ClientType.Gold
    };
    
    private IMongoDatabase _db;
    private IMongoCollection<Client> _clients;
    private int _id;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        (_id, _db) = DatabasesManager.GetDatabase();
        _clients = _db.GetCollection<Client>(CollectionName);
    }
    
    [Test, Order(0)]
    public void InsertDocument_Success()
    {
        Assert.DoesNotThrow(() => _clients.InsertOne(Client1));
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
        _clients.Find(_ => true).CountDocuments().Should().Be(1);
    }
    
    [Test, Order(3)]
    public void CheckDocument_Success()
    {
        var client = _clients.Find(_ => true).First();

        client.FirstName.Should().Be(Client1.FirstName);
        client.LastName.Should().Be(Client1.LastName);
        client.Archived.Should().Be(Client1.Archived);
        client.ClientType.Should().Be(Client1.ClientType);
        client.AccountBalance.Should().Be(Client1.AccountBalance);
    }

    [Test, Order(4)]
    public void UpdateDocument_Success()
    {
        var client = _clients.Find(_ => true).First();

        _clients.ReplaceOne(c => c.Id == client.Id, Client2 with {Id = client.Id});
    }
    
    [Test, Order(5)]
    public void CheckDocumentAfterUpdate_Success()
    {
        var client = _clients.Find(_ => true).First();

        client.FirstName.Should().Be(Client2.FirstName);
        client.LastName.Should().Be(Client2.LastName);
        client.Archived.Should().Be(Client2.Archived);
        client.ClientType.Should().Be(Client2.ClientType);
        client.AccountBalance.Should().Be(Client2.AccountBalance);
    }

    [Test, Order(6)]
    public void RemoveDocument_Success()
    {
        var client = _clients.Find(_ => true).First();

        _clients.DeleteOne(c => c.Id == client.Id);
    }
    
    [Test, Order(7)]
    public void GetAllDocumentsAfterDelete_Success()
    {
        _clients.Find(_ => true).CountDocuments().Should().Be(0);
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