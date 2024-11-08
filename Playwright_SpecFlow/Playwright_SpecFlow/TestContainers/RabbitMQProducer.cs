using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;


namespace Playwright_SpecFlow.TestContainers
{
    public class RabbitMQProducer
    {
        private readonly IModel _channel;

        public RabbitMQProducer(IModel channel)
        {
            _channel = channel;
        }

        // Método para enviar mensajes con diferentes estructuras
        public void ProduceMessage(string messageType, object messageContent)
        {

            string message;

            // Determinar el tipo de mensaje y su estructura
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

            // Enviar el mensaje a RabbitMQ
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "test_queue", basicProperties: null, body: body);
            Console.WriteLine($"[x] Sent {message}");
        }

    }
}
