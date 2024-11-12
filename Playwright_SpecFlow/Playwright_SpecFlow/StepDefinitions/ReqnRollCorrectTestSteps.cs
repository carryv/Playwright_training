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
            //Implementar bucle de reintentos para obterner una respuesta valida
            int maxRetries = 3;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                _apiResponse = await _apiPage.GetApiQuestions(Url);

                if (_apiResponse != null && _apiResponse.Results != null && _apiResponse.Results.Any())
                {
                    break;
                }

                TestContext.WriteLine($"Attempt {retryCount + 1}: No data found. Retrying...");
                await Task.Delay(2000); // Esperar 2 segundos antes de reintentar
                retryCount++;
            }

            Assert.IsNotNull(_apiResponse, "Failed to fetch data from the API after multiple attempts.");
            Assert.IsNotNull(_apiResponse.Results, "Data from the API response results are null.");
            Assert.IsNotEmpty(_apiResponse.Results, "No questions found in data from the API response.");
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
