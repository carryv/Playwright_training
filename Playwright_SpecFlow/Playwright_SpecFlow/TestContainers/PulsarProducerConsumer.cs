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
using DotPulsar.Internal;


namespace Playwright_SpecFlow.TestContainers
{
    public class PulsarProducerConsumer
    {
        private IPulsarClient _client;
        private IProducer<ReadOnlySequence<byte>> _producer;
        private IConsumer<ReadOnlySequence<byte>> _consumer;
        private Config _config = new Config();
        private readonly string _topic = $"persistent://public/default/mytopic";

        public async Task SetupPulsar()
        {
            await _config.PulsarContainerSetup();
        }

        public async Task ProduceConsumer()
        {
            _client = PulsarClient.Builder()
                    .ServiceUrl(new Uri(_config.ServiceUrl))
                    .Build();

            // Crear productor y consumidor 
            _producer = _client.NewProducer()
                                         .Topic(_topic)
                                         .Create();

            _consumer = _client.NewConsumer()
                                         .Topic(_topic)
                                         .SubscriptionName("test-subscription")
                                         .SubscriptionType(SubscriptionType.Exclusive)
                                         .Create();

            await Task.Delay(5000);


        }

        public async Task ProduceMessage()
        {
            var messageProd = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("{\"type\": \"simple\", \"content\": \"Hello, Pulsar World\"}"));

            await _producer.Send(messageProd);
            Console.WriteLine("Message enviado:" + messageProd);
            await _producer.DisposeAsync();
            
        }

        public async Task<string> ConsumeMessage()
        {
            var message = await _consumer.Receive();
            var byteArray = message.Data.ToArray();
            var receivedMessage = Encoding.UTF8.GetString(byteArray);
            Console.WriteLine("Message received:" + receivedMessage);
            await _consumer.Acknowledge(message);
            await _consumer.DisposeAsync();
            return receivedMessage;
        }
    }

    /* public class PulsarTests
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
*/


}

