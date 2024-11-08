using Playwright_SpecFlow.TestContainers;
using Playwright_SpecFlow.Hooks;
using RabbitMQ.Client;
using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class RabbitMQTestsSteps
    {
        private RabbitMQSetup _rabbitMqSetup;
        private RabbitMQConsumer _rabbitMqConsumer;
        private RabbitMQProducer _rabbitMqProducer;
        private readonly ScenarioContext _scenarioContext;

        public RabbitMQTestsSteps(ScenarioContext scenarioContext, RabbitMQSetup rabbitMQSetup)
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
            var simpleContent = "Hello, World";
            _rabbitMqProducer.ProduceMessage("simple", simpleContent);
        }

        [Then(@"the consumer should receive a message with the same content and structure")]
        public void ThenIShouldReceiveAMessageWithTheSameContentAndStructure()
        {
            var msg = _rabbitMqConsumer.ConsumeMessage();
            Console.WriteLine(msg);
        }

        [When(@"the producer sends a complex message'")]
        public void WhenTheProducerSendsAComplexMessage()
        {
            var complexContent = new { text = "Hello", number = 123 };
            _rabbitMqProducer.ProduceMessage("complex", complexContent);
        }

        [When(@"the producer sends a list message'")]
        public void WhenTheProducerSendsAListMessage()
        {
            var listContent = new[] { "item1", "item2", "item3" };
            _rabbitMqProducer.ProduceMessage("list", listContent);
        }



    }
}

