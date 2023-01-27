using System.Text;
using System.Text.Json;

namespace Cinema.Entity.Kafka;

public class KafkaConsts
{
    public static readonly Encoding Encoding = Encoding.ASCII;
    public static readonly JsonSerializerOptions JsonConsumerOptions = new ()
    {
        WriteIndented = true
    };
    public static readonly JsonSerializerOptions JsonProducerOptions = new()
    {
        WriteIndented = false,
        IncludeFields = false,
    };
    public static readonly HeaderNames HeaderNames = HeaderNames.Default;
    public static readonly TopicNames TopicNames = TopicNames.Default;
}