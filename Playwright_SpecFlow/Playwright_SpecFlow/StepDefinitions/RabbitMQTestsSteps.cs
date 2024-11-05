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

        public RabbitMQTestsSteps(ScenarioContext scenarioContext,RabbitMQSetup rabbitMQSetup)
        {
            _scenarioContext = scenarioContext;
            _rabbitMqSetup = rabbitMQSetup;

        }
        [Given(@"a RabbitMQ producer is running")]
        public async Task GivenARabbitMQProducerIsRunning()
        {
            IModel _channel = _rabbitMqSetup.Channel;

            //Set Producer/Consumer
            _rabbitMqConsumer = new RabbitMQConsumer(_channel);
            _rabbitMqProducer = new RabbitMQProducer(_channel);

        }
        [When(@"the producer send a simple message")]
        public void WhenTheProducerSendASimpleMessage()
        {
            var message = "Hello World";
            _rabbitMqProducer.ProduceMessage(message);
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
            var msg = _rabbitMqConsumer.ConsumeMessage();
            Console.WriteLine(msg);
        }

        [Then(@"the consumer should receive a message with the expected list elements")]
        public async Task ThenIShouldReceiveAMessageWithTheExpectedListElements()
        {
            throw new PendingStepException();
        }
    }
}

