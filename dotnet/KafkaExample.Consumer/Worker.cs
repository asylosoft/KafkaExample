using Confluent.Kafka;
using System.Text.Json;

namespace KafkaExample.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ConsumerConfig _config;

        private const string TOPIC = "topic";

        public Worker(ILogger<Worker> logger, ConsumerConfig config)
        {
            _logger = logger;
            _config = config;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<Null, string>(_config).Build())
            {
                consumer.Subscribe(TOPIC);
                try
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume(stoppingToken);
                        Console.WriteLine(JsonSerializer.Serialize(consumeResult));
                    }
                }
                catch (Exception)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }

                return Task.CompletedTask;
            }
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    if (_logger.IsEnabled(LogLevel.Information))
            //    {
            //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    }
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
