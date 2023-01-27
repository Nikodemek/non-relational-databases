using System;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Entity.Interfaces;

namespace Cinema.Consumer.Kafka.Interfaces;

public interface IKafkaConsumer<TEntity> : IDisposable
    where TEntity : class, IMongoEntity<TEntity>
{
    Task<FullResponse<string, string, TEntity>> ConsumeSingleAsync(CancellationToken cancellationToken);
    string GetMemberId();
    string GetName();
}