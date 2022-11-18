using MongoDB.Driver;

namespace Cinema.Data;

public static class CinemaDb
{
    private const string ConnectionString = "mongodb://localhost:27017";
    private const string DatabaseName = "Cinema";

    public static IMongoDatabase Database
    {
        get
        {
            if (_database is not null) return _database;

            var client = new MongoClient(ConnectionString);
            return _database = client.GetDatabase(DatabaseName);
        }
    }

    private static IMongoDatabase? _database;
}