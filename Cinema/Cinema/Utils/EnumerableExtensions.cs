namespace Cinema.Utils;

public static class EnumerableExtensions
{
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (T element in collection)
        {
            action(element);
        }

        return collection;
    }
}