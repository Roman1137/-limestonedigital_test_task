using System.Collections;
using limestonedigital_task.model;

namespace limestonedigital_task.tests
{
    public class DataProvider
    {
        public static IEnumerable CorrectCredentials
        {
            get
            {
                yield return new AccountModel
                {
                    Login = "example123456789example1@gmail.com",
                    Password = "123456789p"
                };
            }
        }
    }
}
