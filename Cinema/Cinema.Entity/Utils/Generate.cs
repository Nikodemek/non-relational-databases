using MongoDB.Bson;

namespace Cinema.Entity.Utils;

internal static class Generate
{
    public static string Id()
    {
        var objectId = ObjectId.GenerateNewId();
        return objectId.ToString();
    }
}