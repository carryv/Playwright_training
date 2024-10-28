using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace Playwright_SpecFlow.TestContainers
{
    public class RabbitMQConsumer
    {
        private readonly IModel _channel;
        private string _receivedMessage;

        public RabbitMQConsumer(IModel channel)
        {
            _channel = channel;
        }

        public string ConsumeMessage()
        {
            var consumer = new EventingBasicConsumer(_channel);
            string receivedMessage = null;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                receivedMessage = Encoding.UTF8.GetString(body);
            };

            _channel.BasicConsume(queue: "test_queue", autoAck: true, consumer: consumer);

            // Esperar a que el mensaje sea consumido
            Task.Delay(500).Wait();
            return receivedMessage;
        }
    }

}
