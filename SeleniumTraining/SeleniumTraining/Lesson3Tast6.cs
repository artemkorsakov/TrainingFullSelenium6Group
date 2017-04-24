using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson3Tast6
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.FirefoxNightly);
        }

        [Test]
        public void CheckFirefoxNightly()
        {
            _driver.Url = "http://localhost:8080/litecart/admin/";
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Login("admin", "admin");
            adminPage.Logout();
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}