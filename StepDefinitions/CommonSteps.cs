using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AutomationDemo.StepDefinitions
{
    [Binding]
    public class CommonSteps : BaseSteps
    {
        private const string ADMIN_APP_XPATH = "//span[normalize-space()='Admin App(form based)']";

        [Given(@"I am on the Admin App Page")]
        public void GivenIAmOnTheAdminAppPage()
        {
            Driver.FindElement(By.XPath(ADMIN_APP_XPATH)).Click();
        }
    }
}
