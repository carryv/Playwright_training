using System;
using Reqnroll;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class ReqnRollCorrectTestSteps
    {
        [Given("User send a request to {string}")]
        public void GivenUserSendARequestTo(string p0)
        {
            throw new PendingStepException();
        }

        [Then("the response should contain at least one {string}: {string}")]
        public void ThenTheResponseShouldContainAtLeastOne(string p0, string @true)
        {
            throw new PendingStepException();
        }

        [Then("print all questions with {string}: {string}")]
        public void ThenPrintAllQuestionsWith(string p0, string @true)
        {
            throw new PendingStepException();
        }
    }
}
