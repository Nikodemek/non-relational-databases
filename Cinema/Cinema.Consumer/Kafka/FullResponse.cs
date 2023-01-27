using Cinema.Entity.Interfaces;
using Confluent.Kafka;

namespace Cinema.Consumer.Kafka;

public class FullResponse<TKey, TValue, TEntity>
    where TEntity : class, IMongoEntity<TEntity>
{
    public bool Success { get; init; }
    public TEntity? Entity { get; init; }
    public ConsumeResult<TKey, TValue>? ConsumeResult { get; init; }
}