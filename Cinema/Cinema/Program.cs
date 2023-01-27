using Cinema;
using Cinema.Entity;
using Cinema.Kafka;
using Cinema.Kafka.Interfaces;
using Cinema.Repository;
using Cinema.Repository.Interfaces;
using Cinema.Services;
using Cinema.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

ConfigureMongoDb(builder.Configuration, args);

var services = builder.Services;
services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

RegisterRepositories(services);
RegisterServices(services);

services.AddSingleton<IKafkaProducer<Order>, KafkaProducer<Order>>();
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


static void RegisterRepositories(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<IClientsRepository, ClientsRepository>();
    serviceCollection.AddSingleton<IAddressesRepository, AddressesRepository>();
    serviceCollection.AddSingleton<IMoviesRepository, MoviesRepository>();
    serviceCollection.AddSingleton<IScreeningsRepository, ScreeningsRepository>();
    serviceCollection.AddSingleton<ITicketsRepository, TicketsRepository>();
    serviceCollection.AddSingleton<IOrdersRepository, OrdersRepository>();
}

static void RegisterServices(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<IClientService, ClientService>();
    serviceCollection.AddSingleton<IAddressService, AddressService>();
    serviceCollection.AddSingleton<IMovieService, MovieService>();
    serviceCollection.AddSingleton<IScreeningService, ScreeningService>();
    serviceCollection.AddSingleton<ITicketService, TicketService>();
    serviceCollection.AddSingleton<IOrderService, OrderService>();
}

static void ConfigureMongoDb(IConfiguration configuration, string[] strings)
{
    var mongoConfigurationSection = configuration.GetSection("MongoDb");
    string? connectionString = mongoConfigurationSection.GetValue<string?>(Consts.ConnectionStringArgName);
    string? databaseName = mongoConfigurationSection.GetValue<string?>(Consts.DatabaseNameArgName);

    CinemaConnection.Configure(strings, (connectionString, databaseName));
}






