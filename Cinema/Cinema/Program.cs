using Cassandra.Mapping;
using Cinema;
using Cinema.Data;
using Cinema.Mappers;
using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Models.Interfaces;
using Cinema.Services;
using Cinema.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

CinemaDb.SetUpConnection(config);
MappingConfiguration.Global.Define<CassandraMappings>();

services
    .AddSingleton<IAddresses, Addresses>()
    .AddSingleton<IClients, Clients>()
    .AddSingleton<IMovies, Movies>()
    .AddSingleton<IScreenings, Screenings>()
    .AddSingleton<ITickets, Tickets>()
    .AddSingleton<IOrders, Orders>();
services
    .AddSingleton<IEntityMapper<Address, AddressDto>, AddressMapper>()
    .AddSingleton<IEntityMapper<Client, ClientDto>, ClientMapper>()
    .AddSingleton<IEntityMapper<Movie, MovieDto>, MovieMapper>()
    .AddSingleton<IEntityMapper<Screening, ScreeningDto>, ScreeningMapper>()
    .AddSingleton<IEntityMapper<Ticket, TicketDto>, TicketMapper>()
    .AddSingleton<IEntityMapper<Order, OrderDto>, OrderMapper>();
services.AddSingleton<TestData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

/*var testData = app.Services.GetService<TestData>();
testData?.InsertData();*/

app.Run();
