using System;
using Confluent.Kafka;

namespace SampleRedPanda.Console;

class Program
{
    static void Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9015",
            ClientId = "redpanda-producer",
            Acks = Acks.All
        };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            var topic = "msg-produce"; // Substitua pelo nome do tópico desejado

            for (var i = 0; i < 10; i++)
            {
                var message = $"Mensagem {i}";

                var deliveryResult = producer.ProduceAsync(topic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
                System.Console.WriteLine($"Mensagem enviada: {message} | Offset: {deliveryResult.Offset}");
            }

            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}
