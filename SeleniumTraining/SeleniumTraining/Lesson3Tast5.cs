﻿using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages.AdminPages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson3Tast5
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.VeryOldFirefox);
        }

        [Test]
        public void CheckOldFirefox()
        {
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Open();
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