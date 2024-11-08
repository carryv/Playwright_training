using NUnit.Framework;
using Playwright_SpecFlow.Hooks;
using Playwright_SpecFlow.TestContainers;
using System;
using Reqnroll;

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


        [When(@"Pulsar producer send a simple message")]
        public async Task WhenPulsarProducerSendASimpleMessage()
        {
            var PulsarSimpleContent = "Hello, Pulsar World";
            await _pulsarSetup.ProduceMessage("simple", PulsarSimpleContent);
        }

        [Then(@"Pulsar consumer should receive a message")]
        public async Task ThenPulsarConsumerShouldReceiveAMessage()
        {
            await _pulsarSetup.ConsumeMessage();
           // Console.WriteLine("receivedMessage");
        }

        [When(@"Pulsar producer send a complex message'")]
        public async Task WhenPulsarProducerSendAComplexMessage()
        {
            var PulsarComplexContent = new { text = "Hello Pulsar", number = 123 };
            await _pulsarSetup.ProduceMessage("complex", PulsarComplexContent);
        }

        [When(@"Pulsar producer send a list message'")]
        public async Task WhenPulsarProducerSendAListMessage()
        {
            var PulsarListContent = new[] { "item1", "item2", "item3" };
            await _pulsarSetup.ProduceMessage("list", PulsarListContent);
        }

    }
}
