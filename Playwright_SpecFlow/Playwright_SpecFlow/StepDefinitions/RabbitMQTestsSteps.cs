using Playwright_SpecFlow.TestContainers;
using RabbitMQ.Client;
using System;
using TechTalk.SpecFlow;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class RabbitMQTestsSteps

    {

        Config _rabbitMqContainer = new Config();
        RabbitMQProducerConsumer _rabbitMq = new RabbitMQProducerConsumer();

        [Given(@"a RabbitMQ producer is running")]
        public async Task GivenARabbitMQProducerIsRunning()
        {
            await _rabbitMqContainer.RabbitMqContainerSetup();
        }

        [When(@"the producer send a message with content '([^']*)'")]
        public async Task WhenISendAMessageWithContent(string p0)
        {
            await _rabbitMq.ProduceMessage("Hola mundo");
        }
        [Then(@"the consumer should receive a message with the same content and structure")]
        public async Task ThenIShouldReceiveAMessageWithTheSameContentAndStructure()
        {
            throw new PendingStepException();
        }

        [Then(@"the consumer should receive a message with the expected list elements")]
        public async Task ThenIShouldReceiveAMessageWithTheExpectedListElements()
        {
            throw new PendingStepException();
        }
    }
}
