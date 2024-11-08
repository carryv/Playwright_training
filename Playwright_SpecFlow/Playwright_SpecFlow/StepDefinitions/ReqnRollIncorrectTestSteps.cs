using System;
using Playwright_SpecFlow.Support;
using Playwright_SpecFlow.Pages;
using Reqnroll;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class ReqnRollIncorrectTestSteps
    {
        private APIResponse _apiResponse;
        private readonly OpenApiPage _apiPage;
        private readonly ScenarioContext _scenarioContext;
        private const string SpecificQuestion = "Which car manufacturer won the 2017 24 Hours of Le Mans?";
        private string _correctAnswer;

        public ReqnRollIncorrectTestSteps()
        {
            _apiPage = new OpenApiPage();
        }

        [Given("User sends a request to {string}")]
        public async Task GivenUserSendsARequestTo(string Url)
        {
            _apiResponse = await _apiPage.GetApiQuestions(Url);
            Assert.IsNotNull(_apiResponse, "Failed to fetch data from the API.");
        }

        [When("User receives the API response")]
        public async Task WhenUserReceivesTheAPIResponse()
        {
            Assert.IsTrue(_apiResponse.Results.Any(), "API response contains no questions.");
        }

        [Then("each question should have exactly three incorrect answers")]
        public void ThenEachQuestionShouldHaveExactlyThreeIncorrectAnswers()
        {
            bool allQuestionsHaveThreeIncorrectAnswers = _apiPage.ValidateThreeIncorrectAnswers(_apiResponse);
            Assert.IsTrue(allQuestionsHaveThreeIncorrectAnswers, "Not all questions have exactly 3 incorrect answers.");
        }


        [Then("extract and print {string} for the question {string}")]
        public async Task ThenExtractAndPrintForTheQuestion(string answerField, string question)
        {
            _correctAnswer = _apiPage.GetCorrectAnswerForQuestion(_apiResponse, question);
            Assert.IsNotNull(_correctAnswer, $"The question '{question}' was not found.");
            TestContext.WriteLine($"Correct answer for '{question}': {_correctAnswer}");
        }

    }
}
