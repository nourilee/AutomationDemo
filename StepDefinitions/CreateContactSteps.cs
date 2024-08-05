using AutomationDemo.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace AutomationDemo.StepDefinitions
{
    [Binding]
    public class CreateContactSteps : BaseSteps
    {

        private const string NAME_FIELD_XPATH = "//input[@id=':rm:' and @name='name']";
        private const string ADDRESS_FIELD_XPATH = "//input[@id=':r11:' and @name='address']";
        private const string COUNTRY_FIELD_XPATH = "//input[@id=':r13:' and @name='country_of_residence']";
        private const string EMAIL_FIELD_XPATH = "//input[@id=':r15:' and @name='email']";
        private const string PHONE_FIELD_XPATH = "//input[@id=':rv:' and @name='phone_number']";
        private const string GENDER_FIELD_XPATH = "//div[@id=':rs:' and @role='combobox']";
        private const string GENDER_OPTION_OTHER_XPATH = "//ul[@id=':rt:']/li[@data-value='Other']";
        private const string ADD_BUTTON_XPATH = "//button[@type='submit' and text()='Add']";
        private const string LAST_ROW_XPATH = "//div[@role='rowgroup']//div[@role='row'][last()]";

        private string name;
        private string address;
        private string email;
        private string phone;


        [When(@"I create a new contact")]
        public void WhenICreateANewContact()
        {
            // Generate new contact data
            name = TestDataGenerator.GenerateName();
            address = TestDataGenerator.GenerateAddress();
            email = TestDataGenerator.GenerateEmail();
            phone = TestDataGenerator.GeneratePhoneNumber();

            // Fill out the add new contact form
            Driver.FindElement(By.XPath(NAME_FIELD_XPATH)).SendKeys(name);
            Driver.FindElement(By.XPath(ADDRESS_FIELD_XPATH)).SendKeys(address);
            Driver.FindElement(By.XPath(COUNTRY_FIELD_XPATH)).SendKeys("USA");
            Driver.FindElement(By.XPath(EMAIL_FIELD_XPATH)).SendKeys(email);
            Driver.FindElement(By.XPath(PHONE_FIELD_XPATH)).SendKeys(phone);
            Driver.FindElement(By.XPath(GENDER_FIELD_XPATH)).Click();
            Driver.FindElement(By.XPath(GENDER_OPTION_OTHER_XPATH)).Click();

            // Submit the form
            Driver.FindElement(By.XPath(ADD_BUTTON_XPATH)).Click();
        }

        [Then(@"the contact should be created successfully")]
        public void ThenTheContactShouldBeCreatedSuccessfully()
        {
            // Locate and click on the last row of the customer list
            var lastRow = Driver.FindElement(By.XPath(LAST_ROW_XPATH));
            Assert.That(lastRow.Displayed, Is.Not.Null);
            lastRow.Click();

            var contactLocator = By.XPath($"//div[contains(text(),'{name}')]");

            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                var contactElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(contactLocator));
                Assert.That(contactElement.Displayed, Is.True);
            }
            catch (NoSuchElementException)
            {
                Assert.Fail($"Contact: '{name}' was not created successfully");
            }
            finally
            {
                Driver?.Quit();
            }
        }
    }
}
