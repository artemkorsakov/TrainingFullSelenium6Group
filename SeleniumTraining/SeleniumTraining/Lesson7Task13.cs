using System;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Entities;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson7Task13
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
        }

        /// <summary>
        /// Сценарий работы с корзиной
        /// </summary>
        [Test]
        public void CheckBasket()
        {
            const int count = 10;
            var sizes = new string[] { "Small", "Medium", "Large" };

            MainPage mainPage = new MainPage(_driver);
            mainPage.Open();
            mainPage.ClickCategory("Rubber Ducks");

            // Покупаем уточек
            for (int i = 0; i < count; i++)
            {
                mainPage.ClickRandomProduct();
                mainPage.AddToCart(new Random().Next(1, count), sizes[new Random().Next(sizes.Length)]);
                mainPage.CloseProductPopup();
            }

            // Удаляем уточек
            mainPage.OpenCart();
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(_driver);
            shoppingCartPage.DeleteAllProduct();
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}