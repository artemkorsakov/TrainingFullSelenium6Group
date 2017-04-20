using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson2Task3
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
        }

        [Test]
        public void FirstTest()
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