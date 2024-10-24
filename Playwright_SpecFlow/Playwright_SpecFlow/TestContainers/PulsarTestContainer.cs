using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Buffers;


namespace Playwright_SpecFlow.TestContainers
{
    public class PulsarProducerConsumer
    {
        private readonly IPulsarClient _client;

        public PulsarProducerConsumer(IPulsarClient client)
        {
            _client = client;
        }

        public async Task ProduceMessage(string topic, string message)
        {
            var producer = _client.NewProducer(Schema.String)
                                  .Topic(topic)
                                  .Create();

            await producer.Send(message);
            await producer.DisposeAsync();
        }

        public async Task<string> ConsumeMessage(string topic)
        {
            var consumer = _client.NewConsumer(Schema.String)
                                  .Topic(topic)
                                  .SubscriptionName("test-subscription")
                                  .Create();

            var message = await consumer.Receive();
            var byteArray = message.Data.ToArray();
            var receivedMessage = Encoding.UTF8.GetString(byteArray);
            await consumer.Acknowledge(message);
            await consumer.DisposeAsync();
            return receivedMessage;
        }
    }

    public class PulsarTests
    {
        private Config _pulsarSetup;
        private PulsarProducerConsumer _producerConsumer;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _pulsarSetup = new Config();
            await _pulsarSetup.PulsarContainerSetup();
            _producerConsumer = new PulsarProducerConsumer(_pulsarSetup.Client);
        }

        public async Task SendSimpleMessage()
        {
            var message = "{\"type\": \"simple\", \"content\": \"Hello, Pulsar World\"}";
            await _producerConsumer.ProduceMessage("test-topic", message);

            var receivedMessage = await _producerConsumer.ConsumeMessage("test-topic");
            Assert.AreEqual(message, receivedMessage);
        }

        public async Task SendComplexMessage()
        {
            var message = "{\"type\": \"complex\", \"content\": {\"text\": \"Hello Pulsar\", \"number\": 123}}";
            await _producerConsumer.ProduceMessage("test-topic", message);

            var receivedMessage = await _producerConsumer.ConsumeMessage("test-topic");
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }

        public async Task SendListMessage()
        {
            var message = "{\"type\": \"list\", \"content\": [\"item1\", \"item2\", \"item3\"]}";
            await _producerConsumer.ProduceMessage("test-topic", message);

            var receivedMessage = await _producerConsumer.ConsumeMessage("test-topic");
            var receivedJson = JObject.Parse(receivedMessage);
            var expectedJson = JObject.Parse(message);

            Assert.AreEqual(expectedJson.ToString(), receivedJson.ToString());
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _pulsarSetup.TearDown();
        }
    }


}

