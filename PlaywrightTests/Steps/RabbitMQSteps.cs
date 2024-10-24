using RabbitMQ.Client;
using System;
using TechTalk.SpecFlow;

namespace PlaywrightTests.Steps
{
    [Binding]
    public class RabbitMQSteps

    {
        [Given(@"a RabbitMQ producer is running")]
        public void GivenARabbitMQProducerIsRunning()
        {
            throw new PendingStepException();
        }

        [When(@"the producer send a message with content '([^']*)'")]
        public void WhenISendAMessageWithContent(string p0)
        {
            throw new PendingStepException();
        }
        [Then(@"the consumer should receive a message with the same content and structure")]
        public void ThenIShouldReceiveAMessageWithTheSameContentAndStructure()
        {
            throw new PendingStepException();
        }

        [Then(@"the consumer should receive a message with the expected list elements")]
        public void ThenIShouldReceiveAMessageWithTheExpectedListElements()
        {
            throw new PendingStepException();
        }
    }
}
