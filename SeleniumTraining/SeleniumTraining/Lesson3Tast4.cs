﻿using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson3Tast4
    {
        [Test]
        public void FirstTest()
        {
            BrowserKind[] browsers = { BrowserKind.Firefox, BrowserKind.Chrome, BrowserKind.IE };
            foreach (var browser in browsers)
            {
                IWebDriver driver = DriverFactory.CreateWebDriver(browser);
                driver.Url = "http://localhost:8080/litecart/admin/";
                AdminPage adminPage = new AdminPage(driver);
                adminPage.Login("admin", "admin");
                adminPage.Logout();
                driver.Quit();
            }
        }
    }
}