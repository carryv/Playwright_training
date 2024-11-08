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

        // Método para enviar diferentes tipos de mensajes
        public async Task ProduceMessage(string messageType, object messageContent)
        {
            string message;

            // Construir el mensaje en función del tipo
            switch (messageType)
            {
                case "simple":
                    message = $"{{\"type\": \"simple\", \"content\": \"{messageContent}\"}}";
                    break;
                case "complex":
                    var complexContent = (dynamic)messageContent;
                    message = $"{{\"type\": \"complex\", \"content\": {{\"text\": \"{complexContent.text}\", \"number\": {complexContent.number}}}}}";
                    break;
                case "list":
                    var listContent = string.Join("\", \"", (string[])messageContent);
                    message = $"{{\"type\": \"list\", \"content\": [\"{listContent}\"]}}";
                    break;
                default:
                    throw new ArgumentException("Invalid message type");
            }

            var messageBytes = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes(message));
            await _producer.Send(messageBytes);
            Console.WriteLine("Message sent: " + message);
        }

        public async Task<string> ConsumeMessage()
        {
            var message = await _consumer.Receive();
            var byteArray = message.Data.ToArray();
            var receivedMessage = Encoding.UTF8.GetString(byteArray);
            Console.WriteLine("Message received: " + receivedMessage);
            await _consumer.Acknowledge(message);
            return receivedMessage;
        }
    }



}

