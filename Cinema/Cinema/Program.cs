using Cinema;
using Cinema.Data;
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
services
    .AddTransient<IClients, Clients>()
    .AddTransient<IAddresses, Addresses>()
    .AddTransient<IMovies, Movies>()
    .AddTransient<IScreenings, Screenings>()
    .AddTransient<ITickets, Tickets>()
    .AddTransient<IOrders, Orders>();
services.AddSingleton<TestData>();
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = config.GetConnectionString("Redis.Configuration");
    options.InstanceName = config.GetConnectionString("Redis.InstanceName");
});

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
