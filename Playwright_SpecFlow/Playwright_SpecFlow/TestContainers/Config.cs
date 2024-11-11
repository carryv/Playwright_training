using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using RabbitMQ.Client;
using Testcontainers.RabbitMq;
using DotPulsar;
using NUnit.Framework;


namespace Playwright_SpecFlow.TestContainers
{
    public class Config
    {
        private IContainer _mysqlContainer;
        private IContainer _rabbitMqContainer;
        private IContainer _pulsarContainer;
        public string ServiceUrl;
        public string RabbitMqConnectionString;
        public string _constring;
        public IConnection Connection { get; private set; }
        public IModel Channel { get; private set; }
        public PulsarClient Client { get; private set; }

        public async Task ContainerSetup()
        {
            _mysqlContainer = new ContainerBuilder()
                .WithImage("mysql:latest")
                .WithPortBinding(3306, true)
                .WithEnvironment("MYSQL_ROOT_PASSWORD", "root")
                .WithEnvironment("MYSQL_DATABASE", "saucedemo_db")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306)) // Esperamos hasta que el puerto esté disponible
                .Build();

            await _mysqlContainer.StartAsync(); // Inicia el contenedor
            var host = _mysqlContainer.Hostname;
            var port = _mysqlContainer.GetMappedPublicPort(3306);
            _constring = $"Server={host};Port={port};Database=saucedemo_db;User=root;Password=root;";
        }

        public async Task RabbitMqContainerSetup()
        {
            _rabbitMqContainer = new RabbitMqBuilder()
                .WithImage("rabbitmq:3.11")  // Usamos la imagen oficial de RabbitMQ
                .WithPortBinding(5672, true) // Puerto aleatorio para evitar conflictos
                .WithUsername("guest")
                .WithPassword("guest")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
                .Build();

            await _rabbitMqContainer.StartAsync();  // Iniciar el contenedor
            var host = _rabbitMqContainer.Hostname;
            var port = _rabbitMqContainer.GetMappedPublicPort(5672);
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

        public async Task PulsarContainerSetup()
        {
         _pulsarContainer = new ContainerBuilder()
            .WithImage("apachepulsar/pulsar:latest")
            .WithName($"pulsar-test-container-{Guid.NewGuid()}") //Dar nombre distintos a cada contenedor
            .WithPortBinding(6650, true) // Asigna un puerto aleatorio
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6650))
            .WithCommand("/bin/bash", "-c", "bin/pulsar standalone")
            .Build();

            await _pulsarContainer.StartAsync();
            await _pulsarContainer.ExecAsync(new[] { "/bin/bash", "-c", "bin/pulsar-admin namespaces create public/default" });

            var host = _pulsarContainer.Hostname;
            var port = _pulsarContainer.GetMappedPublicPort(6650); // Obtener el puerto asignado
            ServiceUrl = $"pulsar://{host}:{port}";

        }

        public async Task TearDown()
        {
            if (_mysqlContainer != null)
            {
                await _mysqlContainer.StopAsync();
                await _mysqlContainer.DisposeAsync();
            }

            if (_rabbitMqContainer != null)
            {
                await _rabbitMqContainer.StopAsync();
                await _rabbitMqContainer.DisposeAsync();
            }

            if (_pulsarContainer != null)
            {
                await _pulsarContainer.StopAsync();
                await _pulsarContainer.DisposeAsync();
            }

        }

    }
}
