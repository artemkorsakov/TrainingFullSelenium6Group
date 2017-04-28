using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Главная страница
    /// </summary>
    internal class MainPage
    {
        private readonly IWebDriver _driver;

        internal MainPage(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        internal void Open()
        {
            _driver.Url = "http://localhost:8080/litecart/";
        }

        /// <summary>
        /// Получить имена всех категорий товаров
        /// </summary>
        internal string[] GetCategoriesName()
        {
            var menus = _driver.FindElements(By.CssSelector("#box-category-tree > ul a"));
            return menus.Select(menu => menu.Text).ToArray();
        }

        /// <summary>
        /// Кликнуть на заданную категорию
        /// </summary>
        internal void ClickCategory(string name)
        {
            _driver.FindElement(By.XPath($"//div[@id='box-category-tree']//a[contains(., '{name}')]")).Click();
            // Ждем, когда пункт меню станет выбранным
            _driver.FindElement(By.XPath($"//div[@id='box-category-tree']//li[contains(@class, 'active')][a[contains(., '{name}')]]"));
        }

        /// <summary>
        /// Проверить, что у каждого товара на странице только один стикер
        /// </summary>
        internal void AssertStickers()
        {
            var products = _driver.FindElements(By.CssSelector(".products .product"));
            foreach (var product in products)
            {
                var countStickers = product.FindElements(By.CssSelector(".sticker")).Count;
                Assert.True(countStickers == 1);
            }
        }
    }
}