using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System;
using OpenQA.Selenium.Support.UI;

namespace AutomationDemo.StepDefinitions
{
    [Binding]
    public class DeleteContactSteps : BaseSteps
    {
        private const string LAST_ROW_XPATH = "//div[@role='rowgroup']//div[@role='row'][last()]";
        private const string EMAIL_DIV_XPATH = "//div[@role='rowgroup']//div[@role='row'][last()]//div[@data-field='email']";
        private const string DELETE_BUTTON_XPATH = "//button[@type='button' and text()='Delete user']";

        private string _emailValue;

        [When(@"I delete a random contact")]
        public void WhenIDeleteARandomContact()
        {
            // Locate and click on the last row of the customer list
            var lastRow = Driver.FindElement(By.XPath(LAST_ROW_XPATH));
            Assert.That(lastRow.Displayed, Is.Not.Null);
            lastRow.Click();

            // Get the email value of the contact
            var emailCell = Driver.FindElement(By.XPath(EMAIL_DIV_XPATH));
            _emailValue = emailCell.Text;
            Console.WriteLine($"Contact: '{_emailValue}' will be deleted.");

            // Click the Delete button
            Driver.FindElement(By.XPath(DELETE_BUTTON_XPATH)).Click();
            Driver.Navigate().Refresh();
        }

        [Then(@"the contact should be deleted successfully")]
        public void ThenTheContactShouldBeDeletedSuccessfully()
        {
            var contactLocator = By.XPath($"//div[contains(text(),'{_emailValue}')]");

            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(contactLocator));
                Assert.Fail($"Contact: '{_emailValue}' was not deleted successfully.");
            }
            catch (WebDriverException ex)
            {
                // Handle the WebDriverException
                Console.WriteLine($"WebDriverException caught: {ex.Message}");
            }
        }
    }
}
