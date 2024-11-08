using System;
using Reqnroll;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class ReqnRollIncorrectTestSteps
    {
        [Given("User send a request to {string}")]
        public void GivenUserSendARequestTo(string p0)
        {
            throw new PendingStepException();
        }

        [When("User receive the incurrect response")]
        public void WhenUserReceiveTheIncurrectResponse()
        {
            throw new PendingStepException();
        }

        [Then("each question should have exactly {int} incorrect answers")]
        public void ThenEachQuestionShouldHaveExactlyIncorrectAnswers(int p0)
        {
            throw new PendingStepException();
        }

        [Then("extract and print {string} for the question {string}")]
        public void ThenExtractAndPrintForTheQuestion(string p0, string p1)
        {
            throw new PendingStepException();
        }
    }
}
