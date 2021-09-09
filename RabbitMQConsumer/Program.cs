using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Create a connection factory
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            //Create a channel
            using var channel = connection.CreateModel();
            //QueueConsumer.Consume(channel);
            //DirectExchangeConsumer.Consume(channel);
            //TopicExchangeConsumer.Consume(channel);
            //HeaderExchangeConsumer.Consume(channel);
            FanoutExchangeConsumer.Consume(channel);
        }
    }
}




