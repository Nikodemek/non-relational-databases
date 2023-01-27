using MongoDB.Driver;
using Parsevoir;

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

    public static void Configure(string[] arguments, (string? connectionString, string? databaseName)? fallbackConfig = null)
    {
        var (connectionString, databaseName) = ParseDatabaseArgs(arguments);
        
        connectionString ??= fallbackConfig?.connectionString;
        databaseName ??= fallbackConfig?.databaseName;
        
        _connectionString = !String.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : throw new ArgumentException("Value was Null or Empty!", nameof(connectionString));
        _databaseName = !String.IsNullOrWhiteSpace(databaseName)
            ? databaseName
            : throw new ArgumentException("Value was Null or Empty!", nameof(databaseName));

        _configured = true;
    }

    private static (string? ConnectionString, string? DatabaseName) ParseDatabaseArgs(string[] arguments)
    {
        string? connectionString = GetValue(Consts.ConnectionStringArgName);
        string? databaseName = GetValue(Consts.DatabaseNameArgName);

        return (connectionString, databaseName);

        
        string? GetValue(string argName)
        {
            string template = $"{argName}={{0}}";
            string? value = arguments.SingleOrDefault(s => s.StartsWith(argName));

            return value is not null
                ? Parse.Single<string>(value, template)
                : null;
        }
    }
}