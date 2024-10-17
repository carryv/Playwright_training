using Newtonsoft.Json;
using NUnit.Framework;
using Microsoft.Playwright;

namespace PlaywrightTests.API
{
    internal class GetBooking
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
        public async Task GetBookingIds()
        {
            var response = await _request.GetAsync("/booking");
            Assert.AreEqual(200, response.Status);

            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(await response.TextAsync());
            Assert.IsTrue(jsonResponse.Count > 0); // Validar que se devuelven IDs
        }

        [TearDown]
        public async Task TearDown()
        {
            await _request.DisposeAsync();
        }
    }
}