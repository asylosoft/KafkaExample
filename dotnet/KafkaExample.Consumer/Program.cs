using Confluent.Kafka;
using KafkaExample.Consumer;

var builder = Host.CreateApplicationBuilder(args);

var config = new ConsumerConfig()
{
    BootstrapServers = "localhost:9092",
    GroupId = "GroupId"
};

builder.Services.AddSingleton(config);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
