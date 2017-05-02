using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.Entities;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Главная страница
    /// </summary>
    internal class MainPage
    {
        protected readonly IWebDriver Driver;

        internal MainPage(IWebDriver driver)
        {
            Driver = driver;
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        internal void Open()
        {
            Driver.Url = "http://localhost:8080/litecart/";
        }

        /// <summary>
        /// Получить имена всех категорий товаров
        /// </summary>
        internal string[] GetCategoriesName()
        {
            var menus = Driver.FindElements(By.CssSelector("#box-category-tree > ul a"));
            return menus.Select(menu => menu.Text).ToArray();
        }

        /// <summary>
        /// Кликнуть на заданную категорию
        /// </summary>
        internal void ClickCategory(string name)
        {
            Driver.FindElement(By.XPath($"//div[@id='box-category-tree']//a[contains(., '{name}')]")).Click();
            // Ждем, когда пункт меню станет выбранным
            Driver.FindElement(By.XPath($"//div[@id='box-category-tree']//li[contains(@class, 'active')][a[contains(., '{name}')]]"));
        }

        /// <summary>
        /// Проверить, что у каждого товара на странице только один стикер
        /// </summary>
        internal void AssertStickers()
        {
            var products = Driver.FindElements(By.CssSelector(".products .product"));
            foreach (var product in products)
            {
                var countStickers = product.FindElements(By.CssSelector(".sticker")).Count;
                Assert.True(countStickers == 1);
            }
        }

        /// <summary>
        /// Получить первый продукт на главной странице и проверить его корректность
        /// </summary>
        internal Product GetFirstProductAndCheckIt()
        {
            var product = Driver.FindElement(By.CssSelector("#box-campaigns .product"));
            string name = product.FindElement(By.CssSelector(".name")).Text;
            string manufacturer = product.FindElement(By.CssSelector(".manufacturer")).Text;

            var regularPrice = product.FindElement(By.CssSelector(".regular-price"));
            string textRegularPrice = regularPrice.Text;
            var campaignPrice = product.FindElement(By.CssSelector(".campaign-price"));
            string textCampaignPrice = campaignPrice.Text;

            AssertProduct(regularPrice, campaignPrice);

            return new Product(name, manufacturer, textRegularPrice, textCampaignPrice);
        }

        /// <summary>
        /// Кликнуть на первый продукт на главной странице
        /// </summary>
        internal void ClickFirstProduct()
        {
            Driver.FindElement(By.CssSelector("#box-campaigns .product > a")).Click();
            Driver.FindElement(By.CssSelector("#box-product"));
        }

        /// <summary>
        /// Получить первый продукт на главной странице и проверить его корректность
        /// </summary>
        internal Product GetSelectedProductAndCheckIt()
        {
            var product = Driver.FindElement(By.CssSelector("#box-product"));
            string name = product.FindElement(By.CssSelector(".title")).Text;
            string manufacturer = product.FindElement(By.CssSelector(".manufacturer img")).GetAttribute("title");

            var regularPrice = product.FindElement(By.CssSelector(".regular-price"));
            string textRegularPrice = regularPrice.Text;
            var campaignPrice = product.FindElement(By.CssSelector(".campaign-price"));
            string textCampaignPrice = campaignPrice.Text;

            AssertProduct(regularPrice, campaignPrice);

            return new Product(name, manufacturer, textRegularPrice, textCampaignPrice);
        }

        /// <summary>
        /// Проверить корректность первого продукта:
        /// в) обычная цена зачёркнутая и серая(можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
        /// г) акционная жирная и красная(можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
        /// (цвета надо проверить на каждой странице независимо, при этом цвета на разных страницах могут не совпадать)
        /// г) акционная цена крупнее, чем обычная(это тоже надо проверить на каждой странице независимо)
        /// </summary>
        protected void AssertProduct(IWebElement regularPrice, IWebElement campaignPrice)
        {
            // обычная цена зачёркнутая
            string styleRegularPrice = regularPrice.GetCssValue("text-decoration-line");

            if (Driver.GetType().Name == "InternetExplorerDriver")
            {
                // В моем браузере IE не определяется стиль "text-decoration-line" (((
                Assert.True(regularPrice.TagName == "s" || regularPrice.TagName == "del");
            }
            else
            {
                Assert.True(styleRegularPrice == "line-through");
            }

            // обычная цена серая(можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
            string colorRegularPrice = regularPrice.GetCssValue("color");
            Rgba rgbaRegularPrice = new Rgba(colorRegularPrice);
            Assert.True(rgbaRegularPrice.IsGrey());

            // акционная жирная
            string styleCampaignPrice = campaignPrice.TagName;
            Assert.True(styleCampaignPrice == "strong");

            // акционная красная(можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
            string colorCampaignPrice = campaignPrice.GetCssValue("color");
            Rgba rgbaCampaignPrice = new Rgba(colorCampaignPrice);
            Assert.True(rgbaCampaignPrice.IsRed());

            // акционная цена крупнее, чем обычная
            string sizeRegularPrice = regularPrice.GetCssValue("font-size").Replace("px", "");
            string sizeCampaignPrice = campaignPrice.GetCssValue("font-size").Replace("px", "");
            Assert.True(double.Parse(sizeCampaignPrice, CultureInfo.InvariantCulture) > double.Parse(sizeRegularPrice, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Кликнуть на регистрацию нового пользователя
        /// </summary>
        internal void ClickRegistration()
        {
            Driver.FindElement(By.XPath("//a[.='New customers click here']")).Click();
            // Ждем, когда откроется страница регистрации
            Driver.FindElement(By.CssSelector("#box-create-account"));
        }

        /// <summary>
        /// Залогироваться под пользователем
        /// </summary>
        internal void Login(string email, string password)
        {
            Driver.FindElement(By.CssSelector("[name=login_form] [name=email]")).SendKeys(email);
            Driver.FindElement(By.CssSelector("[name=login_form] [name=password]")).SendKeys(password);
            Driver.FindElement(By.CssSelector("[name=login_form] button[name=login]")).Click();

            // Надо дождаться, что сохранение прошло успешно
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#box-account")));
        }

        /// <summary>
        /// Разлогироваться
        /// </summary>
        internal void Logout()
        {
            Driver.FindElement(By.XPath("//div[@id='box-account']//a[.='Logout']")).Click();

            // Надо дождаться разлогирования
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("form[name=login_form]")));
        }
    }
}