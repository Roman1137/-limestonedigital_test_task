using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace limestonedigital_task.pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.driver.Manage().Window.Maximize();
        }

        public void WaitUntilIsDisplayed(string locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20))
            {
                Message = $"Locator {locator} should have been displayed at the page"
            };

            wait.Until(x => x.FindElement(By.CssSelector(locator)).Displayed);
        }

        public void EnterText(IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }
    }
}
