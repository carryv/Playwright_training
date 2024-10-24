using System;
using TechTalk.SpecFlow;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class PulsarTestsSteps
    {
        [When(@"I send a message with content '([^']*)'")]
        public async Task WhenISendAMessageWithContent(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"I should receive a message with the same content and structure")]
        public async Task ThenIShouldReceiveAMessageWithTheSameContentAndStructure()
        {
            throw new PendingStepException();
        }

        [Then(@"I should receive a message with the expected list elements")]
        public async Task ThenIShouldReceiveAMessageWithTheExpectedListElements()
        {
            throw new PendingStepException();
        }
    }
}
