using System;
using limestonedigital_task.pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Events;

namespace limestonedigital_task.app
{
    public class Application
    {
        private EventFiringWebDriver driver;

        public GoogleDriveCommonPage GoogleDriveCommonPage;
        public GoogleSignInPage GoogleSignInPage;
        public MyDrivePage MyDrivePage;
        public DocumentPage DocumentPage;

        public Application()
        {
            driver = new EventFiringWebDriver(new FirefoxDriver());
            this.GoogleDriveCommonPage = new GoogleDriveCommonPage(driver);
            this.GoogleSignInPage = new GoogleSignInPage(driver);
            this.MyDrivePage = new MyDrivePage(driver);
            this.DocumentPage = new DocumentPage(driver);

            InitializeEvents();
        }

        public void Quit()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var fileName = TestContext.CurrentContext.TestDirectory + "\\" +
                               DateTime.Now.ToString("yy-MM-dd-HH-mm-ss-FFF") + "-" + GetType().Name + "-" +
                               TestContext.CurrentContext.Test.FullName + "." + ScreenshotImageFormat.Jpeg;
                try
                {
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
                    TestContext.AddTestAttachment(fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            driver.Close();
            driver.Quit();
            driver.Dispose();
            driver = null;
        }

        public void InitializeEvents()
        {
            driver.FindingElement += (sender, args) => Console.WriteLine($"Looking for elemet {args.FindMethod}");
            driver.FindElementCompleted += (sender, args) => Console.WriteLine($"Element {args.FindMethod} was found");
            driver.ElementClicking += (sender, args) => Console.WriteLine($"Clicking element {args.Element}");
            driver.ElementClicked += (sender, args) => Console.WriteLine($"Element {args.Element} was clicked");
            driver.Navigating += (sender, args) => Console.WriteLine($"Navigating to {args.Url}");
            driver.Navigated += (sender, args) => Console.WriteLine($"Navigated to {args.Url}");
        }
    }
}
