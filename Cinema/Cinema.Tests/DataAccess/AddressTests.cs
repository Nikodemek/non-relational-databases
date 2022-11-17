using Cinema.Models;
using FluentAssertions;
using MongoDB.Driver;

namespace Cinema.Tests.DataAccess;

public class AddressTests
{
    private const string CollectionName = "Addresss";
    
    private static readonly Address Address1 = TestData.Address1;
    private static readonly Address Address2 = TestData.Address1 with
    {
        City = "Poznań", Number = "12"
    };
    
    private IMongoDatabase _db;
    private IMongoCollection<Address> _address;
    private int _id;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        (_id, _db) = DatabasesManager.GetDatabase();
        _address = _db.GetCollection<Address>(CollectionName);
    }
    
    [Test, Order(0)]
    public void InsertDocument_Success()
    {
        Assert.DoesNotThrow(() => _address.InsertOne(Address1));
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
        _address.Find(_ => true).CountDocuments().Should().Be(1);
    }
    
    [Test, Order(3)]
    public void CheckDocument_Success()
    {
        var address = _address.Find(_ => true).First();

        address.Country.Should().Be(Address1.Country);
        address.City.Should().Be(Address1.City);
        address.Street.Should().Be(Address1.Street);
        address.Number.Should().Be(Address1.Number);
    }

    [Test, Order(4)]
    public void UpdateDocument_Success()
    {
        var address = _address.Find(_ => true).First();

        _address.ReplaceOne(a => a.Id == address.Id, Address2 with {Id = address.Id});
    }
    
    [Test, Order(5)]
    public void CheckDocumentAfterUpdate_Success()
    {
        var address = _address.Find(_ => true).First();

        address.Country.Should().Be(Address2.Country);
        address.City.Should().Be(Address2.City);
        address.Street.Should().Be(Address2.Street);
        address.Number.Should().Be(Address2.Number);
    }

    [Test, Order(6)]
    public void RemoveDocument_Success()
    {
        var address = _address.Find(_ => true).First();

        _address.DeleteOne(a => a.Id == address.Id);
    }
    
    [Test, Order(7)]
    public void GetAllDocumentsAfterDelete_Success()
    {
        _address.Find(_ => true).CountDocuments().Should().Be(0);
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