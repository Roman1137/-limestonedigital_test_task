using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace limestonedigital_task.pages
{
    public class GoogleDriveCommonPage : BasePage
    {
        public GoogleDriveCommonPage(IWebDriver driver) : base(driver) { }


        public void Open()
        {
            driver.Url = "https://www.google.com/drive/";
            WaitUntilLoaded();
        }

        public void GoToMyDrivePage()
        {
            this.GoToGoogleDriveButtonElement.Click();
        }

        private void WaitUntilLoaded() => WaitUntilIsDisplayed(GoToGoogleDriveButtonLocator);

        private string GoToGoogleDriveButtonLocator => "[data-g-action=Intro]";
        public IWebElement GoToGoogleDriveButtonElement => driver.FindElement(By.CssSelector(this.GoToGoogleDriveButtonLocator));

    }
}
