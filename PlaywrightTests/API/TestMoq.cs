using Newtonsoft.Json;
using NUnit.Framework;
using Microsoft.Playwright;
using Moq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using RestSharp;
using System.Net;

namespace PlaywrightTests.API
{
    internal class TestMoq
    {
        public void GetBookingIds_ShouldReturnMockedResponse()
        {
            // Crear un mock del cliente de RestSharp
            var mockClient = new Mock<IRestClient>();
            var mockedResponse = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "[{\"bookingid\": 1234}, {\"bookingid\": 5678}]"
            };

            mockClient.Setup(client => client.Execute(It.IsAny<RestRequest>()))
                      .Returns(mockedResponse);

            var client = mockClient.Object;
            var request = new RestRequest("/booking", Method.Get);

            var response = client.Execute(request);
            Assert.AreEqual(200, (int)response.StatusCode);

            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
            Assert.AreEqual(1234, (int)jsonResponse[0].bookingid); // Validar ID simulado
        }
    }
}