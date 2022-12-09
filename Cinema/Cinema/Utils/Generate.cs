using MongoDB.Bson;

namespace Cinema.Utils;

public static class Generate
{
    public static string BsonId()
    {
        var objectId = ObjectId.GenerateNewId();
        return objectId.ToString();
    }
}