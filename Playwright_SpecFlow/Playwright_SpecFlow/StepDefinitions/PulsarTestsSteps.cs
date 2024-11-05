using NUnit.Framework;
using Playwright_SpecFlow.Hooks;
using Playwright_SpecFlow.TestContainers;
using System;
using TechTalk.SpecFlow;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class PulsarTestsSteps
    {

        private readonly ScenarioContext _scenarioContext;

        PulsarProducerConsumer _pulsarSetup = new PulsarProducerConsumer();

        [Given(@"a Pulsar producer is running")]
        public async Task GivenAPulsarProducerIsRunning()
        {
            await _pulsarSetup.SetupPulsar();
            await _pulsarSetup.ProduceConsumer();
        }

        [When(@"send a simple message")]
        public async Task WhenSendASimpleMessage()
        {
            await _pulsarSetup.ProduceMessage();

           
        }

        [Then(@"the consumer should receive a message")]
        public async Task ThenTheConsumerShouldReceiveAMessage()
        {
            await _pulsarSetup.ConsumeMessage();
            Console.WriteLine("receivedMessage");



        }
    }
}
