using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests
{
   /* internal class ExampleLogin : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync("http://eaapp.somee.com/");
        }

        [Test]
        public async Task Test1()
        {

            await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

            await Page.GetByLabel("UserName").ClickAsync();

            await Page.GetByLabel("UserName").FillAsync("admin");

            await Page.GetByLabel("UserName").PressAsync("Tab");

            await Page.GetByLabel("Password").FillAsync("password");

            await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Employee List" }).ClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();

            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Employee" })).ToBeVisibleAsync();

        }
    }*/
}
