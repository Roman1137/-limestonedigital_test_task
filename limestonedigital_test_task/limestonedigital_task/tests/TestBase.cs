using limestonedigital_task.app;
using NUnit.Framework;

namespace limestonedigital_task.tests
{
    public class TestBase
    {
        public Application app;

        [SetUp]
        public void SetUp()
        {
            this.app = new Application();
        }

        [TearDown]
        public void Quit()
        {
            app.Quit();
        }
    }
}
