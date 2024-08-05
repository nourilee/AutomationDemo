using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AutomationDemo.StepDefinitions
{
    [Binding]
    public class BaseSteps
    {
        protected IWebDriver Driver;

        [BeforeScenario]
        public void SetUp()
        {
            // Set Chrome options
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--remote-allow-origins=*");

            // Initialize ChromeDriver
            Driver = new ChromeDriver(chromeOptions);

            // Set implicit wait time and maximize the browser window
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Driver.Manage().Window.Maximize();

            // Open the test application
            Driver.Navigate().GoToUrl("https://basic-crud-app-production.up.railway.app");
        }

        [AfterScenario]
        public void TearDown()
        {
            // Close the browser after each test
            Driver?.Quit();
        }
    }
}
