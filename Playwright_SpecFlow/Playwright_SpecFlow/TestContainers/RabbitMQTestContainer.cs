using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace Playwright_SpecFlow.TestContainers
{
    public class RabbitMQProducerConsumer
    {
        private readonly IModel _channel;

        public RabbitMQProducerConsumer(IModel channel)
        {
            _channel = channel;
        }

        public void ProduceMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "test_queue", basicProperties: null, body: body);
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

    //  [Test]
    public class RabbitMQTests
    {
        private Config _rabbitMqSetup = new Config();
        private RabbitMQProducerConsumer _producerConsumer;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _rabbitMqSetup = new Config();
            await _rabbitMqSetup.RabbitMqContainerSetup();
            _producerConsumer = new RabbitMQProducerConsumer(_rabbitMqSetup.Channel);
        }

        public void SendSimpleMessage()
        {
            var message = "{\"type\": \"simple\", \"content\": \"Hello, World\"}";
            _producerConsumer.ProduceMessage(message);

            var receivedMessage = _producerConsumer.ConsumeMessage();
            Assert.AreEqual(message, receivedMessage);
        }

        public void SendComplexMessage()
        {
            var message = "{\"type\": \"complex\", \"content\": {\"text\": \"Hello\", \"number\": 123}}";
            _producerConsumer.ProduceMessage(message);

            var receivedMessage = _producerConsumer.ConsumeMessage();
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }

        public void SendListMessage()
        {
            var message = "{\"type\": \"list\", \"content\": [\"item1\", \"item2\", \"item3\"]}";
            _producerConsumer.ProduceMessage(message);

            var receivedMessage = _producerConsumer.ConsumeMessage();
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
                  await _rabbitMqSetup.TearDown();
        }
    }
}
