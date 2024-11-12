using Playwright_SpecFlow.Support;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Newtonsoft.Json;

namespace Playwright_SpecFlow.Pages
{
    public class OpenApiPage
    {
        private readonly HttpClient _httpClient;
        private APIResponse _responseData;

        public OpenApiPage()
        {
            _httpClient = new HttpClient();
        }



        // Método para obtener preguntas desde la API
        public async Task<APIResponse> GetApiQuestions(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                // Validar si la respuesta es exitosa
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();

                // Validar si el cuerpo de la respuesta no es vacío
                if (string.IsNullOrEmpty(responseBody))
                {
                    throw new Exception("API response body is empty");
                }

                // Deserializar la respuesta en la clase APIResponse
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseBody);

                // Validar que `Results` no sea null
                if (apiResponse == null || apiResponse.Results == null)
                {
                    throw new Exception("Failed to deserialize API response or no results found");
                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error fetching API data: {ex.Message}");
                return null;
            }
        }


        // Validar si al menos una pregunta tiene "correct_answer" como "True"
        public bool ValidateCorrectAnswerExists(APIResponse apiResponse)
        {
            return apiResponse.Results.Any(q => q.correct_answer == "True");
        }

        // Extraer preguntas con "correct_answer": "True"
        public List<string> ExtractQuestionsWithCorrectAnswerTrue(APIResponse apiResponse)
        {
            return apiResponse.Results
                              .Where(q => q.correct_answer == "True")
                              .Select(q => q.question)
                              .ToList();
        }

        // Validar que todas las preguntas tienen exactamente 3 respuestas incorrectas
        public bool ValidateThreeIncorrectAnswers(APIResponse apiResponse)
        {
            return apiResponse.Results.All(q => q.incorrect_answers.Length == 3);

        }
        // Obtener la respuesta correcta para una pregunta específica
        public string GetCorrectAnswerForQuestion(APIResponse apiResponse, string question)
        {
            var matchedQuestion = apiResponse.Results.FirstOrDefault(q => q.question.Contains(question));
            return matchedQuestion?.correct_answer;
        }
    }
}

