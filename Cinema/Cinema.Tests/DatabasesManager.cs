using System.Collections.Concurrent;
using MongoDB.Driver;

namespace Cinema.Tests;

public static class DatabasesManager
{
    private const string ConnectionString = "mongodb://localhost:27017";
    private const string DatabaseBaseName = "Cinema";
    
    private static readonly IMongoClient Client = new MongoClient(ConnectionString);
    private static readonly ConcurrentDictionary<int, string> DatabaseNames = new();
    private static readonly object clientLock = new();
    private static readonly object counterLock = new();
    
    private static int _counter = 0;

    public static (int id, IMongoDatabase database) GetDatabase()
    {
        int id;
        lock (counterLock)
        {
            id = _counter;
            _counter++;
        }
        string databaseName = GetDatabaseName(id);
        IMongoDatabase database;
        lock (clientLock)
        {
            database = Client.GetDatabase(databaseName);
        }
        
        DatabaseNames.TryAdd(id, databaseName);
        return (id, database);
    }

    public static void DeleteDatabase(int id)
    {
        string databaseName = DatabaseNames[id];
        DeleteDatabase(databaseName);
        DatabaseNames.TryRemove(id, out _);
    }

    public static void DeleteDatabase(string databasename)
    {
        lock (clientLock)
        {
            Client.DropDatabase(databasename);
        }
    }
    
    private static string GetDatabaseName(int count) => $"TEST-{DatabaseBaseName}-{count}";
}