using Cinema.Services;
using Cinema.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddTransient<IClients, Clients>();
services.AddTransient<IAddresses, Addresses>();
services.AddTransient<IMovies, Movies>();
services.AddTransient<IScreenings, Screenings>();
services.AddTransient<ITickets, Tickets>();
services.AddTransient<IOrders, Orders>();

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

app.Run();