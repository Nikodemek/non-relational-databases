using MongoDB.Driver;

namespace Cinema.Repository;

public static class CinemaConnection
{
    private static string? _connectionString;
    private static string? _databaseName;
    private static bool _configured = false;
    private static IMongoDatabase? _database;

    internal static IMongoDatabase Database
    {
        get
        {
            if (_database is not null) return _database;

            if (!_configured)
                throw new Exception("Cinema Connection was not configured!");

            var client = new MongoClient(_connectionString);
            return _database = client.GetDatabase(_databaseName);
        }
    }

    public static void Configure(string connectionString, string databaseName)
    {
        _connectionString = !String.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : throw new ArgumentException("Value was Null or Empty!", nameof(connectionString));
        _databaseName = !String.IsNullOrWhiteSpace(databaseName)
            ? databaseName
            : throw new ArgumentException("Value was Null or Empty!", nameof(databaseName));

        _configured = true;
    }
}