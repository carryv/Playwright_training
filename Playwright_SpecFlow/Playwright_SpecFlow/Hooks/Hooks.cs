using Playwright_SpecFlow.TestContainers;
using Testcontainers.RabbitMq;

namespace Playwright_SpecFlow.Hooks
{

    [Binding]
    public sealed class Hooks
    {
    
        private RabbitMQSetup _rabbitMqSetup;
        private RabbitMQTestContainer _rabbitMqTest;
        private ScenarioContext _scenarioContext;


        public Hooks(RabbitMQSetup rabbitMqSetup) : base()
        {
            _rabbitMqSetup=rabbitMqSetup;
        }

        [Before]
        public async Task setupContainer()
        {
            //Set container
            await _rabbitMqSetup.RabbitMqContainerSetup();

        }

     //   [After]

    }
}