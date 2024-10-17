using Newtonsoft.Json;
using NUnit.Framework;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace PlaywrightTests.API
{
    internal class Token
    {
        private IAPIRequestContext _request;

        [SetUp]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _request = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = "https://restful-booker.herokuapp.com"
            });
        }

        [Test]
        public async Task CreateToken_ShouldReturnValidToken()
        {
            var endpoint = "/auth";
            var response = await _request.PostAsync(endpoint, new APIbody
            {
                DataObject = new { username = "admin", password = "password123" }
            });

            Assert.AreEqual(200, response.Status);
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(await response.TextAsync());
            Assert.IsNotNull(jsonResponse.token);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _request.DisposeAsync();
        }
    }
}

namespace PlaywrightTests
{
    class APIbody : APIRequestContextOptions
    {
       // public var DataObject { get; set; }
    }
}