using DotNet.Testcontainers.Builders;
using RabbitMQ.Client;
using Testcontainers.RabbitMq;

namespace Playwright_SpecFlow.Hooks
{
    public class RabbitMQSetup
    {
        public RabbitMqContainer RabbitMqContainer { get; private set; }
        public IConnection Connection { get; private set; }
        public IModel Channel { get; private set; }

        public async Task RabbitMqContainerSetup()
        {
            RabbitMqContainer = new RabbitMqBuilder()
                .WithImage("rabbitmq:3.11")  // Usamos la imagen oficial de RabbitMQ
                .WithPortBinding(5672, 5672)        // Puerto para enviar/recibir mensajes
                .WithUsername("guest")
                .WithPassword("guest")
                .Build();

            await RabbitMqContainer.StartAsync();

            var factory = new ConnectionFactory()
            {
                HostName = "localhost", //http://localhost:15672/ 
                Port = RabbitMqContainer.GetMappedPublicPort(5672),
                UserName = "guest",
                Password = "guest"
            };

            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.QueueDeclare(queue: "test_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public async Task StopContainer()
        {
            Channel.Close();
            Connection.Close();
            await RabbitMqContainer.StopAsync();
        }
    }


}
