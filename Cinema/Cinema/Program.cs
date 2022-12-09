using Cinema;
using Cinema.Services;
using Cinema.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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
    options.Configuration = "localhost:6379";
    options.InstanceName = "Cinema_"; 
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

var testData = app.Services.GetService<TestData>();
testData?.InsertData();
testData?.InsertData();
testData?.InsertData();
testData?.InsertData();

app.Run();
