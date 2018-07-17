using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace limestonedigital_task.pages
{
    public class DocumentPage : BasePage
    {
        public DocumentPage(IWebDriver driver) : base(driver) { }

        public void RenameDoc(string newName)
        {
            WaitUntilLoaded();

            driver.ExecuteJavaScript("arguments[0].value  = ''", this.EditTitleFieldElement);
            this.EditTitleFieldElement.SendKeys(newName + Keys.Enter);
            WaitIntilDocumentIsSaved();
        }

        public void CloseTab()
        {
            var activeTab = driver.CurrentWindowHandle;
            var allTabs = driver.WindowHandles.ToList();
            allTabs.Remove(activeTab);

            driver.Close();
            driver.SwitchTo().Window(allTabs.Single());
        }

        public string GetDocumentText()
        {
            WaitUntilLoaded();

            return TextFieldElement.GetAttribute("textContent");
        }

        private void WaitIntilDocumentIsSaved(int timeToWait = 5)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWait))
            {
                Message = $"The document was not saved in {timeToWait} seconds"
            };
            wait.Until(dr => SaveTitleLinkElement.Text.Contains("Все изменения сохранены на Диске"));
        }

        private void WaitUntilLoaded() => WaitUntilIsDisplayed(EditTitleFieldLocator);

        private string EditTitleFieldLocator => "[guidedhelpid=editor_title]";
        //private string TextFieldLocator => ".kix-lineview";
        private string TextFieldLocator => ".kix-page-column";
        private string SaveTitleLinkLocator => ".docs-title-save-label-text";

        public IWebElement EditTitleFieldElement => driver.FindElement(By.CssSelector(this.EditTitleFieldLocator));
        public IWebElement TextFieldElement => driver.FindElement(By.CssSelector(this.TextFieldLocator));
        public IWebElement SaveTitleLinkElement => driver.FindElement(By.CssSelector(this.SaveTitleLinkLocator));
    }
}
