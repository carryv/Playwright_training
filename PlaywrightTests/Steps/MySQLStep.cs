using System;
using TechTalk.SpecFlow;

namespace PlaywrightTests.Steps
{
    [Binding]
    public class MySQLStep
    {

        [Given(@"I have a running MySQL Container")]
        public void GivenIHaveARunningMySQLContainer()
        {
            throw new PendingStepException();
        }

        [Then(@"the database should be accessible")]
        public void ThenTheDatabaseShouldBeAccessible()
        {
            throw new PendingStepException();
        }
    }
}
