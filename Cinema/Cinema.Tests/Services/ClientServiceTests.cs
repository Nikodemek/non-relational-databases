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

public class ClientServiceTests
{
    private static readonly Client Client1 = TestData.Client1;
    private static readonly Client Client2 = TestData.Client1 with 
        { Id = Guid.NewGuid(), FirstName = "Paweł" };

    private AddressService _addressService;
    private ClientService _clientService;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        DbManager.Initialize();

        _addressService = new AddressService(NullLogger<AddressService>.Instance, new AddressMapper());
        await _addressService.CreateAsync(Client1.Address!);
        await _addressService.CreateAsync(Client2.Address!);
        
        _clientService = new ClientService(NullLogger<ClientService>.Instance, new ClientMapper(_addressService));
    }
    
    [Test, Order(0)]
    public async Task InsertEntity_Success()
    {
        using var rowSet = await _clientService.CreateAsync(Client1);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(1)]
    public async Task CountEntities_Success()
    {
        var allEntities = await _clientService.GetAllAsync();

        allEntities.Should().HaveCount(1);
        allEntities.Should().Contain(Client1);
    }

    [Test, Order(2)]
    public async Task CheckMapping_Success()
    {
        var entity1 = await _clientService.GetAsync(Client1.Id);
        
        entity1.Id.Should().Be(Client1.Id);
        entity1.FirstName.Should().Be(Client1.FirstName);
        entity1.LastName.Should().Be(Client1.LastName);
        entity1.Birthday.Should().Be(Client1.Birthday);
        entity1.ClientType.Should().Be(Client1.ClientType);
        entity1.Address.Should().BeEquivalentTo(Client1.Address);
        entity1.AccountBalance.Should().Be(Client1.AccountBalance);
        entity1.Archived.Should().Be(Client1.Archived);
    }

    [Test, Order(3)]
    public async Task UpdateEntity_Success()
    {
        using var rowSet = await _clientService.UpdateAsync(Client1.Id, Client2 with { Id = Client1.Id});

        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(4)]
    public async Task CheckUpdate_Success()
    {
        var entity2 = await _clientService.GetAsync(Client1.Id);
        
        entity2.Id.Should().Be(Client1.Id);
        entity2.FirstName.Should().Be(Client2.FirstName);
        entity2.LastName.Should().Be(Client2.LastName);
        entity2.Birthday.Should().Be(Client2.Birthday);
        entity2.ClientType.Should().Be(Client2.ClientType);
        entity2.Address.Should().BeEquivalentTo(Client2.Address);
        entity2.AccountBalance.Should().Be(Client2.AccountBalance);
        entity2.Archived.Should().Be(Client2.Archived);
    }

    [Test, Order(5)]
    public async Task InsertSecond_Success()
    {
        using var rowSet = await _clientService.CreateAsync(Client2);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }
    
    [Test, Order(6)]
    public async Task GetWithMultipleIds_Success()
    {
        var entities = await _clientService.GetAllWithIdsAsync(new []
        {
            Client1.Id,
            Client2.Id
        });

        entities.Should().HaveCount(2);
    }

    [Test, Order(7)]
    public async Task DeleteOne_Success()
    {
        using var rowSet = await _clientService.DeleteAsync(Client1.Id);
        
        rowSet.Should().NotBeNull();
        rowSet.IsFullyFetched.Should().BeTrue();
    }

    [Test, Order(8)]
    public async Task CheckDelete_Success()
    {
        var entities = await _clientService.GetAllAsync();

        entities.Should().HaveCount(1);
    }


    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _clientService.DeleteAsync(Client1.Id);
        await _clientService.DeleteAsync(Client2.Id);
        
        await _addressService.DeleteAsync(Client1.Address!.Id);
        await _addressService.DeleteAsync(Client2.Address!.Id);
    }
}