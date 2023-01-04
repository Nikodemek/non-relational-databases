namespace Cinema.Data;

public static class CinemaDb
{
    private static string? _connectionString;
    private static string? _databaseName;

    public static void SetUpConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Mongo.Configuration");
        _databaseName = configuration.GetConnectionString("Mongo.DatabaseName");
    }
    
    public static void SetUpConnection(string connectionString, string databaseName)
    {
        _connectionString = connectionString;
        _databaseName = databaseName;
    }

    /*public static IMongoDatabase Database
    {
        get
        {
            if (_database is not null) return _database;

            if (_connectionString is null || _databaseName is null)
                throw new Exception("MongoDB connection info was not initialized!");

            var client = new MongoClient(_connectionString);
            return _database = client.GetDatabase(_databaseName);
        }
    }

    private static IMongoDatabase? _database;*/
}