using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Страница администратора
    /// </summary>
    internal class AdminPage
    {
        private readonly IWebDriver _driver;

        internal AdminPage(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        internal void Open()
        {
            _driver.Url = "http://localhost:8080/litecart/admin/";
        }

        /// <summary>
        /// Вход на панель администратора под заданными логином и паролем
        /// </summary>
        internal void Login(string login, string password)
        {
            _driver.FindElement(By.Name("username")).SendKeys(login);
            _driver.FindElement(By.Name("password")).SendKeys(password);
            _driver.FindElement(By.XPath("//button[.='Login']")).Click();
        }

        /// <summary>
        /// Получить имена всех пунктов меню на странице
        /// </summary>
        internal string[] GetMenusName()
        {
            var menus = _driver.FindElements(By.CssSelector("ul#box-apps-menu > li .name"));
            return menus.Select(menu => menu.Text).ToArray();
        }

        /// <summary>
        /// Кликнуть на заданный пункт меню
        /// </summary>
        internal void ClickMenu(string name)
        {
            _driver.FindElement(By.XPath($"//a[span[.='{name}']]")).Click();
            // Ждем, когда пункт меню станет выбранным
            _driver.FindElement(By.XPath($"//li[a[span[.='{name}']]][@class='selected']"));
        }

        /// <summary>
        /// Получить имена всех подпунктов зданного меню на странице
        /// </summary>
        internal string[] GetSubmenusName(string name)
        {
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0);
            var menus = _driver.FindElements(By.XPath($"//li[a[span[.='{name}']]]//li"));
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, DriverFactory.TimeOutSeconds);
            return menus.Select(menu => menu.Text).ToArray();
        }

        /// <summary>
        /// Кликнуть на заданный подпункт меню
        /// </summary>
        internal void ClickSubmenu(string name, string subname)
        {
            _driver.FindElement(By.XPath($"//ul[@class='docs']//a[span[.='{subname}']]")).Click();
            // Ждем, когда пункт меню станет выбранным
            _driver.FindElement(By.XPath($"//ul[@class='docs']/li[a[span[.='{subname}']]][@class='selected']"));
        }

        /// <summary>
        /// Проверяем наличие заголовка страницы
        /// </summary>
        internal void AssertPresentTitle()
        {
            Assert.True(Elements.AreElementsPresent(_driver, By.CssSelector("#main > h1")));
        }

        /// <summary>
        /// Разлогирование с панели администратора
        /// </summary>
        internal void Logout()
        {
            _driver.FindElement(By.XPath("//a[@title='Logout']")).Click();
        }
    }
}