using Cinema;
using Cinema.Repository;
using Cinema.Repository.Interfaces;
using Cinema.Services;
using Cinema.Services.Interfaces;
using Parsevoir;

const string ConnectionStringArgName = "ConnectionString";
const string DatabaseNameArggName = "DatabaseName";

var builder = WebApplication.CreateBuilder(args);

ConfigureDatabase(args, builder.Configuration);

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

void ConfigureDatabase(string[] arguments, ConfigurationManager configurationManager)
{
    var (connectionString, databaseName) = ParseDatabaseArgs(arguments);

    connectionString ??= configurationManager[ConnectionStringArgName];
    databaseName ??= configurationManager[DatabaseNameArggName];
    
    CinemaConnection.Configure(connectionString, databaseName);
}

(string? ConnectionString, string? DatabaseName) ParseDatabaseArgs(string[] arguments)
{
    string? connectionString = GetValue(ConnectionStringArgName);
    string? databaseName = GetValue(DatabaseNameArggName);

    return (connectionString, databaseName);

    string info = "Marcin ma 48 lat, a Kasia ma 3 jabłek";
    string template = "Marcin ma {} lat, a Kasia ma {} jabłek";

    var (marcinAge, appleCount) = Parse.Single<int, int>(info, template);
    
    string? GetValue(string argName)
    {
        string template = $"{argName}={{}}";
        string? value = arguments.SingleOrDefault(s => s.StartsWith(argName));

        return value is null ? null : Parse.Single<string>(value, template);
    }
}






