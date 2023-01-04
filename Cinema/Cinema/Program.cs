using Cassandra.Mapping;
using Cinema;
using Cinema.Data;
using Cinema.Mappers;
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
    .AddSingleton<IClients, Clients>()
    .AddSingleton<IAddresses, Addresses>()
    .AddSingleton<IMovies, Movies>()
    .AddSingleton<IScreenings, Screenings>()
    .AddSingleton<ITickets, Tickets>()
    .AddSingleton<IOrders, Orders>();
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
