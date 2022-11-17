using System.Linq;
using Cinema.Models;
using FluentAssertions;
using MongoDB.Driver;

namespace Cinema.Tests.DataAccess;

public class OrderTests
{
    private const string CollectionName = "Orders";
    
    private static readonly Order Order1 = TestData.Order1;
    private static readonly Order Order2 = TestData.Order1 with
    {
        FinalPrice = 3, Success = true
    };
    
    private IMongoDatabase _db;
    private IMongoCollection<Order> _orders;
    private int _id;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        (_id, _db) = DatabasesManager.GetDatabase();
        _orders = _db.GetCollection<Order>(CollectionName);
    }
    
    [Test, Order(0)]
    public void InsertDocument_Success()
    {
        Assert.DoesNotThrow(() => _orders.InsertOne(Order1));
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
        _orders.Find(_ => true).CountDocuments().Should().Be(1);
    }
    
    [Test, Order(3)]
    public void CheckDocument_Success()
    {
        var order = _orders.Find(_ => true).First();

        order.FinalPrice.Should().Be(Order1.FinalPrice);
        order.Success.Should().Be(Order1.Success);
        order.Tickets.Should().HaveSameCount(Order1.Tickets);
    }

    [Test, Order(4)]
    public void UpdateDocument_Success()
    {
        var order = _orders.Find(_ => true).First();

        _orders.ReplaceOne(o => o.Id == order.Id, Order2 with {Id = order.Id});
    }
    
    [Test, Order(5)]
    public void CheckDocumentAfterUpdate_Success()
    {
        var order = _orders.Find(_ => true).First();

        order.FinalPrice.Should().Be(Order2.FinalPrice);
        order.Success.Should().Be(Order2.Success);
        order.Tickets.Should().HaveSameCount(Order2.Tickets);
    }

    [Test, Order(6)]
    public void RemoveDocument_Success()
    {
        var order = _orders.Find(_ => true).First();

        _orders.DeleteOne(o => o.Id == order.Id);
    }
    
    [Test, Order(7)]
    public void GetAllDocumentsAfterDelete_Success()
    {
        _orders.Find(_ => true).CountDocuments().Should().Be(0);
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