using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Login : PageTest
{
    //private IBrowser _browser;
    private LoginPage _loginPage;

    [SetUp]
    public void setup()
    {
        _loginPage = new LoginPage(Page);
    }



    [Test]
    public async Task HasTitle()
    {
        await _loginPage.GotoLogin();
        await Task.Delay(2000);
        await Expect(Page).ToHaveTitleAsync(new Regex("Swag Labs"));
    }

    [Test]
    public async Task Standard_login()
    {
        await _loginPage.GotoLogin();
        await _loginPage.Login("standard_user", "secret_sauce");
        await Task.Delay(5000);
        await _loginPage.ValidateLoginStandard();

    }
    [Test]
    public async Task Locked_Login()
    {
        await _loginPage.GotoLogin();
        await _loginPage.Login("locked_out_user", "secret_sauce");
        await Task.Delay(5000);
        await _loginPage.ValidateLoginLocked();


    }
}