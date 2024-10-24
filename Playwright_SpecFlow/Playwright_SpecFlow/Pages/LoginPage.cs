using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playwright_SpecFlow.Pages
{
    internal class LoginPage : PageTest
    {
        string username = "input[data-test='username']";
        string pass = "input[data-test='password']";
        string loginBttn = "input[data-test='login-button']";
        public readonly IPage _page;
        public LoginPage(IPage page)
        {
            _page = page;
        }
        private ILocator Username => _page.Locator(username);
        private ILocator Password => _page.Locator(pass);
        private ILocator LoginButton => _page.Locator(loginBttn);



        public async Task GotoLogin()
        {
            Console.WriteLine("entra");
            await _page.GotoAsync("https://www.saucedemo.com/");
            await Task.Delay(2000);
        }


        public async Task Login(string user, string pass)
        {
            await Username.ClickAsync();
            await Username.FillAsync(user);
            await Password.ClickAsync();
            await Password.FillAsync(pass);
            await LoginButton.ClickAsync();
        }



        public async Task ValidateLoginStandard()
        {
            await _page.Locator("[data-test=\"inventory-container\"]").IsVisibleAsync();
        }

        public async Task ValidateLoginLocked()
        {
            await _page.Locator(".error-message-container > h3:nth-child(1)").IsVisibleAsync(); ;

        }

    }
}


