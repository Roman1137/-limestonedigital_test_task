using FluentAssert;
using limestonedigital_task.dataHelper;
using limestonedigital_task.model;
using NUnit.Framework;

namespace limestonedigital_task.tests
{
    [TestFixture]
    public class Tests : TestBase
    {
        [Test, TestCaseSource(typeof(DataProvider), nameof(DataProvider.CorrectCredentials))]
        public void VerifyGoogleDocCreation(AccountModel AccountModel)
        {
            app.GoogleDriveCommonPage.Open();
            app.GoogleDriveCommonPage.GoToMyDrivePage();

            app.GoogleSignInPage.SignIn(AccountModel);

            app.MyDrivePage.OpenCreateItemControl();
            app.MyDrivePage.CreateItemControl.InitiateGoogleDocFileCreation();

            var name = DataHelper.GenerateRandomString();

            app.DocumentPage.RenameDoc(name);
            app.DocumentPage.CloseTab();

            app.MyDrivePage.IsDocDisplayed(name)
                .ShouldBeTrue("The just created document should be displayed in 'MyDrive' page");

            app.MyDrivePage.OpenDocumentByName(name);
        }
    }
}
