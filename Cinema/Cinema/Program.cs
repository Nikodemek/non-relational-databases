using Cinema;
using Cinema.Repository;
using Cinema.Repository.Interfaces;
using Cinema.Services;
using Cinema.Services.Interfaces;
using Parsevoir;

var builder = WebApplication.CreateBuilder(args);

CinemaConnection.Configure(args, builder.Configuration);

var services = builder.Services;
services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

RegisterRepositories(services);

RegisterServices(services);

services.AddSingleton<TestData>();

var app = builder.Build();

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


void RegisterRepositories(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<IClientsRepository, ClientsRepository>();
    serviceCollection.AddSingleton<IAddressesRepository, AddressesRepository>();
    serviceCollection.AddSingleton<IMoviesRepository, MoviesRepository>();
    serviceCollection.AddSingleton<IScreeningsRepository, ScreeningsRepository>();
    serviceCollection.AddSingleton<ITicketsRepository, TicketsRepository>();
    serviceCollection.AddSingleton<IOrdersRepository, OrdersRepository>();
}

void RegisterServices(IServiceCollection serviceCollection)
{
    serviceCollection.AddTransient<IClientService, ClientService>();
    serviceCollection.AddTransient<IAddressService, AddressService>();
    serviceCollection.AddTransient<IMovieService, MovieService>();
    serviceCollection.AddTransient<IScreeningService, ScreeningService>();
    serviceCollection.AddTransient<ITicketService, TicketService>();
    serviceCollection.AddTransient<IOrderService, OrderService>();
}






