using Cinema.Entity.Interfaces;
using Confluent.Kafka;

namespace Cinema.Consumer.Kafka.Interfaces;

public interface IKafkaConsumer<TEntity> : IDisposable
    where TEntity : class, IMongoEntity<TEntity>
{
    Task<FullResponse<string, string, TEntity>> ConsumeSingleAsync(CancellationToken cancellationToken);
}