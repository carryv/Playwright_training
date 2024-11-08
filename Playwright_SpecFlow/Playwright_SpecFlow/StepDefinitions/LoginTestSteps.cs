using System;
using Reqnroll;
using Docker.DotNet.Models;
using Microsoft.Playwright;
using Playwright_SpecFlow.Pages;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class LoginTestSteps

    {
        private readonly LoginPage _loginPage;
        public readonly IPage _page;

        public LoginTestSteps(IPage page)
        {

            _page = page;
            _loginPage = new LoginPage(_page);

        }

        [Given(@"I navigate to the SauceDemo login page")]
        public async Task GivenINavigateToTheSauceDemoLoginPage()
        {
            Console.Write("llega");
            await _loginPage.GotoLogin();
        }

        [When(@"I enter the username and password")]
        public void WhenIEnterTheUsernameAndPassword()
        {
          //  _scenarioContext.Pending();
        }

        [When(@"I click the login button")]
        public async Task WhenIClickTheLoginButton()
        {
            await _loginPage.Login("standard_user", "secret_sauce");
        }

        [Then(@"I should see the expected result")]
        public async Task ThenIShouldSeeThe()
        {
            await _loginPage.ValidateLoginStandard();
        }
    }
}
