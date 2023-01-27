using System;
using System.Threading.Tasks;
using Cinema.Entity;
using Confluent.Kafka;

namespace Cinema.Kafka.Interfaces;

internal interface IKafkaProducer<in TEntity> : IDisposable
{
    Task<DeliveryResult<string, string>> ProduceAsync(TEntity entity);
}