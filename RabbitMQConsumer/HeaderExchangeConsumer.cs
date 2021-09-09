using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQConsumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("Header-Exchange", ExchangeType.Headers);
            channel.QueueDeclare("header-queue", true, false, false, null);

            var header = new Dictionary<string, object>
            {
                {"account","new" }
            };

            channel.QueueBind("header-queue", "Header-Exchange", string.Empty, header);
            channel.BasicQos(0, 10, false);



            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.Write(message + "\n");
            };
            channel.BasicConsume("header-queue", true, consumer);
            Console.WriteLine("Consumer Started");
            Console.ReadLine();
        }
    }
}
