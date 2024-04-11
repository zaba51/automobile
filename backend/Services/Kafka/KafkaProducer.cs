using Confluent.Kafka;
using Confluent.Kafka.Admin;

public class KafkaProducerService
{
    private readonly ProducerConfig _producerConfig;
    private readonly IProducer<string, string> _producer;

    public KafkaProducerService(IConfiguration configuration)
    {
        _producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<string, string>(_producerConfig).Build();
    }

    public async Task ProduceAsync(string topic, string message)
    {
        try
        {
            await CreateTopicIfNotExists(topic);
            var deliveryReport = await _producer.ProduceAsync(topic, new Message<string, string> { Value = message });
            Console.WriteLine($"Delivered message to {deliveryReport.TopicPartitionOffset}");
        }
        catch (Exception)
        {
            Console.WriteLine($"Failed to produce message");
            _producer.Dispose();
        }
    }

    public void Dispose()
    {
        _producer.Dispose();
    }

    public async Task CreateTopicIfNotExists(string topicName)
    {
        try
        {
            using (var adminClient = new AdminClientBuilder(_producerConfig).Build())
            {
                var topicMetadata = adminClient.GetMetadata(topicName, TimeSpan.FromSeconds(15)).Topics.SingleOrDefault();

                bool isTopicExist = false;

                if (topicMetadata != null && (topicMetadata.Error.Code != ErrorCode.UnknownTopicOrPart || topicMetadata.Error.Code == ErrorCode.Local_UnknownTopic))
                    isTopicExist = true;

                if (!isTopicExist)
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[]
                    {
                new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = 1,
                    ReplicationFactor = 1
                }
                    });
                }
            }
        }
        catch (Exception)
        {   
            throw;
        }
    }

    public string GetTopicName(int supplierId)
    {
        return $"transactions_{supplierId}";
    }
}