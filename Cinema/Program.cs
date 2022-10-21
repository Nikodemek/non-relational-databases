using Cinema;
using Cinema.Data;
using Cinema.Models;
using Cinema.Services;
using Cinema.Services.Interfaces;
using Cinema.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContextPool<CinemaContext>(
    options => options.UseSqlServer(Consts.DatabaseConnectionString)
);
services.AddTransient<IClientService, ClientService>();
services.AddTransient<IAddressService, AddressService>();
services.AddTransient<IMovieService, MovieService>();
services.AddTransient<IScreeningService, ScreeningService>();
services.AddTransient<ITicketService, TicketService>();
services.AddTransient<IOrderService, OrderService>();

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