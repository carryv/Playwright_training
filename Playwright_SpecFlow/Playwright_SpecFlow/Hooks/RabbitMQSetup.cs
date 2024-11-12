using DotNet.Testcontainers.Builders;
using RabbitMQ.Client;
using Testcontainers.RabbitMq;

namespace Playwright_SpecFlow.Hooks
{
    public sealed class RabbitMQSetup
    {
        public RabbitMqContainer RabbitMqContainer { get; private set; }
        public IConnection Connection { get; private set; }
        public IModel Channel { get; private set; }

        public string RabbitMqConnectionString;

        public async Task RabbitMqContainerSetup()
        {
            RabbitMqContainer = new RabbitMqBuilder()
                .WithImage("rabbitmq:3.11")  // Usamos la imagen oficial de RabbitMQ
                .WithPortBinding(5672, true) // Puerto aleatorio para evitar conflictos
                .WithUsername("guest")
                .WithPassword("guest")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
                .Build();

            await RabbitMqContainer.StartAsync();  // Iniciar el contenedor
            var host = RabbitMqContainer.Hostname;
            var port = RabbitMqContainer.GetMappedPublicPort(5672);
            RabbitMqConnectionString = $"amqp://guest:guest@{host}:{port}/"; // Guardamos la cadena de conexión

            var factory = new ConnectionFactory()
            {
                HostName = host,
                Port = port,
                UserName = "guest",
                Password = "guest"
            };

            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.QueueDeclare(queue: "test_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public async Task StopContainer()
        {
            if (Channel != null)
            {
                Channel.Close();
                Channel.Dispose();
            }
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }
            if (RabbitMqContainer != null)
            {
                await RabbitMqContainer.StopAsync();
                await RabbitMqContainer.DisposeAsync();
            }
        }
    }


}
