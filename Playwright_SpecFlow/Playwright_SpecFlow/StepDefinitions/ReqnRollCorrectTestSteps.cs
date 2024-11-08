using NUnit.Framework;
using Microsoft.Playwright;
using Playwright_SpecFlow.Support;
using System.Threading.Tasks;
using Playwright_SpecFlow.Pages;


namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class ReqnRollCorrectTestSteps
    {
        private readonly OpenApiPage _apiPage;
        private APIResponse _apiResponse;
        private List<string> _questionsAnswerTrue;

        public ReqnRollCorrectTestSteps()
        {
            _apiPage = new OpenApiPage();
        }
        private readonly ScenarioContext _scenarioContext;

        [Given("User send a request to {string}")]
        public async Task GivenUserSendARequestTo(string Url)
        {
            // Llamamos al método que hace la solicitud GET
            _apiResponse = await _apiPage.GetApiQuestions(Url);

        }
        [When("User check the API response for questions with {string} as {string}")]
        public void WhenUserReceiveASuccessfulResponse(string key, string value)
        {
            //Validar respuesta
            bool exists = _apiPage.ValidateCorrectAnswerExists(_apiResponse);

            Assert.IsTrue(exists, $"No question with {key}: {value} found.");

            // Extraer preguntas con "correct_answer": "True"
            _questionsAnswerTrue = _apiPage.ExtractQuestionsWithCorrectAnswerTrue(_apiResponse);
        }
    

        [Then("the response should contain at least one {string}: {string}")]
        public void ThenTheResponseShouldContainAtLeastOne(string key, string value)
        {
            Assert.IsNotEmpty(_questionsAnswerTrue, $"No questions with {key}: {value} found.");
        }

        [Then("print all questions with correct_answer is True")]
        public void ThenPrintAllQuestionsWithCorrect_AnswerTrue()
        {
            TestContext.WriteLine("Questions with correct_answer is True");
            foreach (var question in _questionsAnswerTrue)
            {
                TestContext.WriteLine(question);
            }
        }

    }
}
