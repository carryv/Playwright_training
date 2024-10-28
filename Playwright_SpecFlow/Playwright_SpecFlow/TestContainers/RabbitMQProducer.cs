using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace Playwright_SpecFlow.TestContainers
{
    public class RabbitMQProducer
    {
        private readonly IModel _channel;

        public RabbitMQProducer(IModel channel)
        {
            _channel = channel;
        }

        public void ProduceMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "test_queue", basicProperties: null, body: body);
            Console.WriteLine($" [x] Sent {message}");
        }

    }
}
