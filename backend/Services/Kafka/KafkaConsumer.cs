using Confluent.Kafka;

public class KafkaConsumerService
{
    private readonly ConsumerConfig _consumerConfig;
    // private readonly IConsumer<Ignore, string> _consumer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public KafkaConsumerService(IConfiguration configuration)
    {
        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "AUTOMOBILE_API",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoOffsetStore = false
        };

        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task<IEnumerable<string>> ConsumeMessagesAsync(string topic)
    {
        using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
        {
            consumer.Subscribe(topic);
            var messages = new List<string>();

            try
            {
                Console.WriteLine($"Starting consuming");

                Task task = Task.Run(async () =>
                {
                    await Task.Delay(1000 * 5);
                    _cancellationTokenSource.Cancel();
                });

                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(_cancellationTokenSource.Token);
                    messages.Add(consumeResult.Message.Value);
                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                }
            }
            catch(ConsumeException) {}
            catch (OperationCanceledException) { }
            finally
            {
                consumer.Close();
                _cancellationTokenSource.Cancel();
                consumer.Dispose();
            }

            return messages;
        }
    }
}