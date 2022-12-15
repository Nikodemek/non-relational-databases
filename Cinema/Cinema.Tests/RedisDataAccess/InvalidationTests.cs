using System.Threading.Tasks;
using Cinema.Data;
using Cinema.Models;
using Cinema.Services;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Logging.Abstractions;

namespace Cinema.Tests.RedisDataAccess;

public class InvalidationTests
{
    private static readonly Address Address1 = TestData.Address1;
    private static readonly Address Address2 = Address1 with {Id = TestData.Ticket1.Id, City = "Innemiasto"};
    
    private IDistributedCache _cache;
    private UniversalCommonsService<Address> _addressService;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        CinemaDb.SetUpConnection(DatabasesManager.ConnectionString, DatabasesManager.DatabaseName);
        
        _cache = new RedisCache(TestData.RedisCacheOptions);
        _addressService = new Addresses(NullLogger<Addresses>.Instance, _cache);
    }
    
    [Test, Order(0)]
    public async Task ClearData_Success()
    {
        await _addressService.DeleteAllAsync(true);
        
        var all = await _addressService.GetAllAsync();
        all.Should().HaveCount(0);
    }

    [Test, Order(1)]
    public async Task InitializeData_Success()
    {
        await _addressService.CreateAsync(Address1);
        await _addressService.CreateAsync(Address2);
        
        var all = await _addressService.GetAllAsync();
        all.Should().HaveCount(2);
    }

    [Test, Order(2)]
    public async Task Invalidate_Success()
    {
        await _addressService.DeleteAllAsync();

        var all = await _addressService.GetAllAsync();
        all.Should().HaveCount(0);
        
        await _addressService.RestoreAllAsync();
        all = await _addressService.GetAllAsync();
        all.Should().HaveCount(2);
    }

}