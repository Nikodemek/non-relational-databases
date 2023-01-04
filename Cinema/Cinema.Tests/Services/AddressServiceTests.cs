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

public class AddressServiceTests
{
    private static readonly Address Address1 = TestData.Address1;
    private static readonly Address Address2 = TestData.Address1 with 
        { Id = Guid.NewGuid(), City = "Poznań" };

    private AddressService _addressService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        DbManager.Initialize();
        
        _addressService = new (NullLogger<AddressService>.Instance, new AddressMapper());
    }
    
    [Test, Order(0)]
    public async Task InsertEntity_Success()
    {
        using var rowSet = await _addressService.CreateAsync(Address1);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(1)]
    public async Task CountEntities_Success()
    {
        var allEntities = await _addressService.GetAllAsync();

        allEntities.Should().HaveCount(1);
        allEntities.Should().Contain(Address1);
    }

    [Test, Order(2)]
    public async Task CheckMapping_Success()
    {
        var entity1 = await _addressService.GetAsync(Address1.Id);
        
        entity1.Id.Should().Be(Address1.Id);
        entity1.Country.Should().Be(Address1.Country);
        entity1.City.Should().Be(Address1.City);
        entity1.Street.Should().Be(Address1.Street);
        entity1.Number.Should().Be(Address1.Number);
    }

    [Test, Order(3)]
    public async Task UpdateEntity_Success()
    {
        using var rowSet = await _addressService.UpdateAsync(Address1.Id, Address2 with { Id = Address1.Id});

        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(4)]
    public async Task CheckUpdate_Success()
    {
        var entity2 = await _addressService.GetAsync(Address1.Id);
        
        entity2.Id.Should().Be(Address1.Id);
        entity2.Country.Should().Be(Address2.Country);
        entity2.City.Should().Be(Address2.City);
        entity2.Street.Should().Be(Address2.Street);
        entity2.Number.Should().Be(Address2.Number);
    }

    [Test, Order(5)]
    public async Task InsertSecond_Success()
    {
        using var rowSet = await _addressService.CreateAsync(Address2);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }
    
    [Test, Order(6)]
    public async Task GetWithMultipleIds_Success()
    {
        var entities = await _addressService.GetAllWithIdsAsync(new []
        {
            Address1.Id,
            Address2.Id
        });

        entities.Should().HaveCount(2);
    }

    [Test, Order(7)]
    public async Task DeleteOne_Success()
    {
        using var rowSet = await _addressService.DeleteAsync(Address1.Id);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(8)]
    public async Task CheckDelete_Success()
    {
        var entities = await _addressService.GetAllAsync();

        entities.Should().HaveCount(1);
    }


    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _addressService.DeleteAsync(Address1.Id);
        await _addressService.DeleteAsync(Address2.Id);
    }
}