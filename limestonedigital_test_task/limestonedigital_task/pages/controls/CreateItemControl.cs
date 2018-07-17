using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace limestonedigital_task.pages.controls
{
    public class CreateItemControl : BasePage
    {
        public CreateItemControl(IWebDriver driver) : base(driver) { }

        public void InitiateGoogleDocFileCreation()
        {
            var allTabs = driver.WindowHandles;
            this.GoogleDocumentsButtonElement.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            {
                Message = "New tab was not opened"
            };
            wait.Until(x => x.WindowHandles.Count == allTabs.Count + 1);
            var newTab = driver.WindowHandles.Except(allTabs);
            driver.SwitchTo().Window(newTab.Single());
        }

        private string CreateItemLocator => ".a-v-T";

        public IWebElement GoogleDocumentsButtonElement => driver.FindElements(By.CssSelector(this.CreateItemLocator))
            .Single(el => el.Text.Contains("Google Документы"));

    }
}
