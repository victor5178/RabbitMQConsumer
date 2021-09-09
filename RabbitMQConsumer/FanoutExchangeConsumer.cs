using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQConsumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("fanout-queue", true, false, false, null);



            channel.QueueBind("fanout-queue", "fanout-exchange", string.Empty);
            channel.BasicQos(0, 10, false);



            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.Write(message + "\n");
            };
            channel.BasicConsume("fanout-queue", true, consumer);
            Console.WriteLine("Consumer Started");
            Console.ReadLine();
        }
    }
}
