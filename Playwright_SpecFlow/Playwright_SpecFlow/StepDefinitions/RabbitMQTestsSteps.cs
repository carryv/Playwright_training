using Playwright_SpecFlow.TestContainers;
using Playwright_SpecFlow.Hooks;
using RabbitMQ.Client;
using System;
using TechTalk.SpecFlow;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class RabbitMQTestsSteps
    {
        private RabbitMQSetup _rabbitMqSetup;
        private RabbitMQConsumer _rabbitMqConsumer;
        private RabbitMQProducer _rabbitMqProducer;
        private RabbitMQTestContainer _rabbitMqTest;
        private readonly ScenarioContext _scenarioContext;

        public RabbitMQTestsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"a RabbitMQ producer is running")]
        public async Task GivenARabbitMQProducerIsRunning()
        {
            //Set container
            _rabbitMqSetup = new RabbitMQSetup();
            await _rabbitMqSetup.RabbitMqContainerSetup();
            //Set Producer/Consumer
            _rabbitMqConsumer = new RabbitMQConsumer(_rabbitMqSetup.Channel);
            _rabbitMqProducer = new RabbitMQProducer(_rabbitMqSetup.Channel);

            _rabbitMqTest = new RabbitMQTestContainer();

        }
        [When(@"the producer send a simple message")]
        public void WhenTheProducerSendASimpleMessage()
        {
            _rabbitMqTest.SendSimpleMessage();
        }
        [When(@"the producer send a message with content '([^']*)'")]
        public void WhenISendAMessageWithContent(string p0)
        {
            //  _rabbitMqTest.SendSimpleMessage();
            //   _rabbitMqTest.SendListMessage();
            //  _rabbitMqTest.SendComplexMessage();

        }

        [Then(@"the consumer should receive a message with the same content and structure")]
        public void ThenIShouldReceiveAMessageWithTheSameContentAndStructure()
        {
            _rabbitMqTest.RecievedSimpleMessage("Hola World!");
        }

        [Then(@"the consumer should receive a message with the expected list elements")]
        public async Task ThenIShouldReceiveAMessageWithTheExpectedListElements()
        {
            throw new PendingStepException();
        }
    }
}

