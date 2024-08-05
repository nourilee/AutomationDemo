using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AutomationDemo.Utilities;
using OpenQA.Selenium.Interactions;

namespace AutomationDemo.StepDefinitions
{
    [Binding]
    public class UpdateContactSteps : BaseSteps
    {
        private const string LAST_ROW_XPATH = "//div[@role='rowgroup']//div[@role='row'][last()]";
        private const string ADDRESS_FIELD_XPATH = "//input[@id=':rf:' and @name='address']";
        private const string UPDATE_BUTTON_XPATH = "//button[@type='submit' and text()='Update']";
        private const string REFRESH_BUTTON_XPATH = "//button[@type='button' and text()='Refresh']";
        private const string ADDRESS_DIV_XPATH = "//div[@role='rowgroup']//div[@role='row'][last()]//div[@data-field='address']";

        private string _newAddress;

        [When(@"I update the contact's address")]
        public void WhenIUpdateTheContactsAddress()
        {
            // Locate and click on the last row of the customer list
            var lastRow = Driver.FindElement(By.XPath(LAST_ROW_XPATH));
            Assert.That(lastRow.Displayed, Is.Not.Null);
            lastRow.Click();

            // Generate new address data
            _newAddress = TestDataGenerator.GenerateAddress();

            // Update the address field
            var addressField = Driver.FindElement(By.XPath(ADDRESS_FIELD_XPATH));
            var actions = new Actions(Driver);
            actions.MoveToElement(addressField).Click().KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control).SendKeys(Keys.Backspace).SendKeys(_newAddress).Perform();

            // Click Update and Refresh buttons
            Driver.FindElement(By.XPath(UPDATE_BUTTON_XPATH)).Click();
            Driver.FindElement(By.XPath(REFRESH_BUTTON_XPATH)).Click();
        }

        [Then(@"the address should be updated successfully")]
        public void ThenTheAddressShouldBeUpdatedSuccessfully()
        {
            // Refresh the page and verify the address update
            Driver.Navigate().Refresh();

            var addressDiv = Driver.FindElement(By.XPath(ADDRESS_DIV_XPATH));
            var updatedAddress = addressDiv.Text;
            Assert.That(updatedAddress, Is.EqualTo(_newAddress), "The address field did not update as expected");
        }
    }
}
