using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Playwright_SpecFlow.Hooks;
using Playwright_SpecFlow.TestContainers;


namespace Playwright_SpecFlow.TestContainers
{
    public class RabbitMQTestContainer
    {
        private RabbitMQSetup _rabbitMqSetup;
        private RabbitMQConsumer _rabbitMqConsumer;
        private RabbitMQProducer _rabbitMqProducer;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _rabbitMqSetup = new RabbitMQSetup();
            await _rabbitMqSetup.RabbitMqContainerSetup();
            _rabbitMqConsumer = new RabbitMQConsumer(_rabbitMqSetup.Channel);
            _rabbitMqProducer = new RabbitMQProducer(_rabbitMqSetup.Channel);
        }

        public void SendSimpleMessage()
        {
            var message = "Hello World";
            _rabbitMqProducer.ProduceMessage(message);

        }

        public void RecievedSimpleMessage(string message)
        {
            var receivedMessage = _rabbitMqConsumer.ConsumeMessage();
            Assert.AreEqual(message, receivedMessage);
        }

        public void SendComplexMessage()
        {
            var message = "{\"type\": \"complex\", \"content\": {\"text\": \"Hello\", \"number\": 123}}";
            _rabbitMqProducer.ProduceMessage(message);

            var receivedMessage = _rabbitMqConsumer.ConsumeMessage();
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }
        public void RecievedComplexMessage(string message)
        {
            var receivedMessage = _rabbitMqConsumer.ConsumeMessage();
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }
        public void SendListMessage()
        {
            var message = "{\"type\": \"list\", \"content\": [\"item1\", \"item2\", \"item3\"]}";
            _rabbitMqProducer.ProduceMessage(message);

            var receivedMessage = _rabbitMqConsumer.ConsumeMessage();
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }

        public void RecievedListMessage(string message)
        {
            var receivedMessage = _rabbitMqConsumer.ConsumeMessage();
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }

        public async Task TearDown()
        {
            await _rabbitMqSetup.StopContainer();
        }
    }
}