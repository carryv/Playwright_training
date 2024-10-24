using System;
using TechTalk.SpecFlow;

namespace PlaywrightTests.Steps
{
    [Binding]
    public class PulsarSteps
    {
        [When(@"I send a message with content '([^']*)'")]
        public void WhenISendAMessageWithContent(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"I should receive a message with the same content and structure")]
        public void ThenIShouldReceiveAMessageWithTheSameContentAndStructure()
        {
            throw new PendingStepException();
        }

        [Then(@"I should receive a message with the expected list elements")]
        public void ThenIShouldReceiveAMessageWithTheExpectedListElements()
        {
            throw new PendingStepException();
        }
    }
}

