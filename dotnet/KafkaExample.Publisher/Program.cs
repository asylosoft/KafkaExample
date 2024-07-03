using Confluent.Kafka;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var config = new ProducerConfig()
{
    BootstrapServers = "localhost:9092"
};

//builder.Services.AddSingleton(config);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapPost("/publish", async (string message) =>
{
    using var producer = new ProducerBuilder<Null, string>(config).Build();
    var result = await producer.ProduceAsync("topic", new Message<Null, string> { Value = message });
    Console.WriteLine(JsonSerializer.Serialize(result));
    return result;
});

app.Run();
