using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson3Tast4
    {
        [Test]
        public void CheckAnyBrowser()
        {
            BrowserKind[] browsers = { BrowserKind.Firefox, BrowserKind.Chrome, BrowserKind.IE };
            foreach (var browser in browsers)
            {
                IWebDriver driver = DriverFactory.CreateWebDriver(browser);
                AdminPage adminPage = new AdminPage(driver);
                adminPage.Open();
                adminPage.Login("admin", "admin");
                adminPage.Logout();
                driver.Quit();
            }
        }
    }
}