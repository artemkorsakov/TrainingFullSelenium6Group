using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages.AdminPages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson10Task17
    {
        private EventFiringWebDriver _driver;

        [SetUp]
        public void Start()
        {
            IWebDriver factory = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
            _driver = new EventFiringWebDriver(factory);
            _driver.FindingElement += (sender, e) => Console.WriteLine(e.FindMethod);
            _driver.FindElementCompleted += (sender, e) => Console.WriteLine(e.FindMethod + " found");
            _driver.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);
        }

        /// <summary>
        /// Сценарий проверки отсутствия сообщений в логе браузера при просмотре продуктов
        /// </summary>
        [Test]
        public void CheckLogBrowser()
        {
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Open();
            adminPage.Login("admin", "admin");
            adminPage.ClickMenu("Catalog");
            adminPage.ClickSubmenu("Catalog");
            CatalogPage catalogPage = new CatalogPage(_driver);
            catalogPage.OpenFolder("Rubber Ducks");

            var ids = catalogPage.GetAllIdsProduct();
            foreach (string id in ids)
            {
                catalogPage.SelectProductById(id);
                Assert.True(_driver.Manage().Logs.GetLog("browser").Count == 0);
                _driver.Navigate().Back();
            }
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}