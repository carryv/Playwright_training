using Playwright_SpecFlow.TestContainers;
using Testcontainers.RabbitMq;

namespace Playwright_SpecFlow.Hooks
{

    [Binding]
    public sealed class Hooks
    {
    
        private RabbitMQSetup _rabbitMqSetup;
        private ScenarioContext _scenarioContext;
        private Config _config;


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

      /*  [After]
        public async Task TearDownContainers()
        {
            if (_rabbitMqSetup != null)
            {
                await _rabbitMqSetup.StopContainer();

            }

        }*/

    }
}