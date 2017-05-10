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
            MainPage mainPage = new MainPage(_driver);
            mainPage.Open();
            mainPage.ClickCategory("Rubber Ducks");

            // Покупаем уточек
            SimpleProduct purpleDuck = new SimpleProduct("Purple Duck", "ACME Corp.");
            SimpleProduct greenDuck = new SimpleProduct("Green Duck", "ACME Corp.");
            SimpleProduct yellowDuck = new SimpleProduct("Yellow Duck", "ACME Corp.");
            mainPage.ClickProduct(purpleDuck);
            mainPage.AddToCart(3);
            mainPage.CloseProductPopup();
            mainPage.ClickProduct(greenDuck);
            mainPage.AddToCart(1);
            mainPage.CloseProductPopup();
            mainPage.ClickProduct(yellowDuck);
            mainPage.AddToCart(2, "Large");
            mainPage.CloseProductPopup();

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