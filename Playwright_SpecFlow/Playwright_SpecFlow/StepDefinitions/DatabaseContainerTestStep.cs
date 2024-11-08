using Playwright_SpecFlow.TestContainers;
using System;
using Reqnroll;

namespace Playwright_SpecFlow.StepDefinitions
{
    [Binding]
    public class DatabaseContainerTestStep
    {

        MySQLTestContainer _mysql = new MySQLTestContainer();

        [Given(@"I have a running MySQL Container")]
        public async Task GivenIHaveARunningMySQLContainer()
        {
           //Config ddbb
            await _mysql.Setup();
        }

        [Then(@"the database should be accessible")]
        public async Task ThenTheDatabaseShouldBeAccessible()
        {
            await _mysql.StartMySqlContainer();
        }
    }
}
