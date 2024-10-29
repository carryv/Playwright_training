

using Playwright_SpecFlow.TestContainers;

namespace Playwright_SpecFlow.Hooks
{

    [Binding]
    public sealed class Hooks
    {
        private RabbitMQSetup _rabbitMqSetup;
        private RabbitMQTestContainer _rabbitMqTest;
        private ScenarioContext _scenarioContext;


        [Before]
        public async Task setupContainer()
        {
            //Set container
            _rabbitMqSetup = new RabbitMQSetup();
            await _rabbitMqSetup.RabbitMqContainerSetup();

        }

       // [After]

    }
}