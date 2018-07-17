using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using limestonedigital_task.pages.controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace limestonedigital_task.pages
{
    public class MyDrivePage : BasePage
    {
        public CreateItemControl CreateItemControl;

        public MyDrivePage(IWebDriver driver) : base(driver)
        {
            this.CreateItemControl = new CreateItemControl(driver);
        }

        public void OpenCreateItemControl()
        {
            WaitUntilLoaded();

            this.CreateButtonElement.Click();
            this.WaitUntilCreateContolIsExpanded();
        }

        public bool IsDocDisplayed(string docName)
        {
            driver.Navigate().Refresh();
            WaitUntilLoaded();

            return this.GoogleDockElements.Any(el => el.Text.Contains(docName));
        }

        private void WaitUntilCreateContolIsExpanded()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(dr => this.CreateButtonElement.GetAttribute(this.ExpandedAreaLocator).Contains("true"));
        }

        public void OpenDocumentByName(string name)
        {
            var actions = new Actions(driver);

            var documentElement = this.GoogleDockElements.Single(el => el.Text.Contains(name));
            actions.ContextClick(documentElement).Perform();

            driver.SwitchTo().DefaultContent();

            OpenWithListItemElement.Click();

            var allTabs = driver.WindowHandles;

            GoogleDocumentsListElement.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            {
                Message = "New tab was not opened"
            };
            wait.Until(x => x.WindowHandles.Count == allTabs.Count + 1);
            var newTab = driver.WindowHandles.Except(allTabs);
            driver.SwitchTo().Window(newTab.Single());
        }

        private void WaitUntilLoaded() => WaitUntilIsDisplayed(this.MyDiscTitleLocator);

        private string CreateButtonLocator => "[guidedhelpid='new_menu_button']";
        private string ExpandedAreaLocator => "aria-expanded";
        private string GoogleDockLocator => ".l-u-xb.l-u-Ab.l-u-Xc-Wa-ka.l-oi-cc";
        private string MyDiscTitleLocator => ".h-sb-Ic.h-R-w-d-ff";
        private string ListItemLocator => ".a-v-T";

        public IWebElement CreateButtonElement => driver.FindElement(By.CssSelector(this.CreateButtonLocator));
        public IReadOnlyCollection<IWebElement> GoogleDockElements => driver.FindElements(By.CssSelector(this.GoogleDockLocator));
        public IWebElement OpenWithListItemElement => driver.FindElements(By.CssSelector(ListItemLocator)).Single(x => x.Text.Contains("Открыть с помощью"));
        public IWebElement GoogleDocumentsListElement => driver.FindElements(By.CssSelector(ListItemLocator)).Single(x => x.Text.Contains("Google Документы"));
    }
}
