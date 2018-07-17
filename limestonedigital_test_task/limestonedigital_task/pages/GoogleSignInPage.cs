using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using limestonedigital_task.model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace limestonedigital_task.pages
{
    public class GoogleSignInPage : BasePage
    {
        public GoogleSignInPage(IWebDriver driver) : base(driver) { }

        public void SignIn(AccountModel model)
        {
            WaitUntilLoaded();

            EnterEmailAndSubmit(model.Login);
            WaitUntilEmailFieldIsDisappeared();
            EnterPasswordAndSumbit(model.Password);
        }

        private void WaitUntilEmailFieldIsDisappeared()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            {
                Message = "Email field should have disappeared aftet clicking [Next button]"
            };
            wait.Until(ExpectedConditions.StalenessOf(EmailFieldElement));
        }

        private void EnterEmailAndSubmit(string login)
        {
            EnterText(EmailFieldElement, login);
            NextButtonAfterLoginElement.Click();
        }

        private void EnterPasswordAndSumbit(string password)
        {
            EnterText(PasswordFieldElement, password);
            NextButtonAfterPasswordElement.Click();
        }

        private void WaitUntilLoaded() => WaitUntilIsDisplayed(this.LoginFormLocator);

        private string LoginFormLocator => ".xkfVF";
        private string EmailFieldLocator => "[type=email]";
        private string NextButtonAfterLoginLocator => "#identifierNext";
        private string PasswordFieldLocator => "[type=password]";
        private string NextButtonAfterPasswordLocator => "#passwordNext";

        public IWebElement EmailFieldElement => driver.FindElement(By.CssSelector(this.EmailFieldLocator));
        public IWebElement NextButtonAfterLoginElement => driver.FindElement(By.CssSelector(this.NextButtonAfterLoginLocator));
        public IWebElement PasswordFieldElement => driver.FindElement(By.CssSelector(this.PasswordFieldLocator));
        public IWebElement NextButtonAfterPasswordElement => driver.FindElement(By.CssSelector(this.NextButtonAfterPasswordLocator));
    }
}
