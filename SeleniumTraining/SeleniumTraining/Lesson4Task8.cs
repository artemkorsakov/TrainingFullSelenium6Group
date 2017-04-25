﻿using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson4Task8
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
        }

        /// <summary>
        /// Сценарий:
        /// Сделайте сценарий, проверяющий наличие стикеров у всех товаров в учебном приложении litecart на главной странице. 
        /// Стикеры -- это полоски в левом верхнем углу изображения товара, на которых написано New или Sale 
        /// или что-нибудь другое. Сценарий должен проверять, что у каждого товара имеется ровно один стикер.
        /// </summary>
        [Test]
        public void CheckFirefoxNightly()
        {
            _driver.Url = "http://localhost:8080/litecart/admin/";
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Login("admin", "admin");
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}