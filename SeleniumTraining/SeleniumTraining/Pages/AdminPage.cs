using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.DriverHelper;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Страница администратора
    /// </summary>
    internal class AdminPage
    {
        protected readonly IWebDriver Driver;

        internal AdminPage(IWebDriver driver)
        {
            Driver = driver;
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        internal void Open()
        {
            Driver.Url = "http://localhost:8080/litecart/admin/";
        }

        /// <summary>
        /// Вход на панель администратора под заданными логином и паролем
        /// </summary>
        internal void Login(string login, string password)
        {
            Driver.FindElement(By.Name("username")).SendKeys(login);
            Driver.FindElement(By.Name("password")).SendKeys(password);
            Driver.FindElement(By.XPath("//button[.='Login']")).Click();
            WaitLoad();
        }

        /// <summary>
        /// Получить имена всех пунктов меню на странице
        /// </summary>
        internal string[] GetMenusName()
        {
            var menus = Driver.FindElements(By.CssSelector("ul#box-apps-menu > li .name"));
            return menus.Select(menu => menu.Text).ToArray();
        }

        /// <summary>
        /// Кликнуть на заданный пункт меню
        /// </summary>
        internal void ClickMenu(string name)
        {
            Driver.FindElement(By.XPath($"//a[span[.='{name}']]")).Click();
            // Ждем, когда пункт меню станет выбранным
            Driver.FindElement(By.XPath($"//li[a[span[.='{name}']]][@class='selected']"));
        }

        /// <summary>
        /// Получить имена всех подпунктов зданного меню на странице
        /// </summary>
        internal string[] GetSubmenusName(string name)
        {
            Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0);
            var menus = Driver.FindElements(By.XPath($"//li[a[span[.='{name}']]]//li"));
            Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, DriverFactory.TimeOutSeconds);
            return menus.Select(menu => menu.Text).ToArray();
        }

        /// <summary>
        /// Кликнуть на заданный подпункт меню
        /// </summary>
        internal void ClickSubmenu(string subname)
        {
            Driver.FindElement(By.XPath($"//ul[@class='docs']//a[span[.='{subname}']]")).Click();
            // Ждем, когда пункт меню станет выбранным
            Driver.FindElement(By.XPath($"//ul[@class='docs']/li[a[span[.='{subname}']]][@class='selected']"));
        }

        /// <summary>
        /// Проверяем наличие заголовка страницы
        /// </summary>
        internal void AssertPresentTitle()
        {
            Assert.True(Elements.AreElementsPresent(Driver, By.CssSelector("#main > h1")));
        }

        /// <summary>
        /// Редактировать заданный параметр
        /// </summary>
        internal void EditKey(string key)
        {
            Driver.FindElement(By.XPath($"//tr[td[.='{key}']]//a[@title='Edit']")).Click();
            // Ожидаем, когда появится кнопка "Сохранить" - так мы поймем, что страница перестроилась для редактирования
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//button[@value='Save']"));
        }

        /// <summary>
        /// Заданному параметру установить заданное булевское значение
        /// </summary>
        internal void SetBooleanKey(string key, bool value)
        {
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//label[normalize-space(.)='{value}']")).Click();
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//label[normalize-space(.)='{value}'][contains(@class, 'active')]"));
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        internal void SaveKey(string key)
        {
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//button[@value='Save']")).Click();

            // Надо дождаться, что сохранение прошло успешно
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div#notices > div.alert-success")));
        }

        /// <summary>
        /// Разлогирование с панели администратора
        /// </summary>
        internal void Logout()
        {
            Driver.FindElement(By.XPath("//a[@title='Logout']")).Click();
        }

        /// <summary>
        /// Ожидание загрузки страницы:
        /// Ждем, когда осуществится вход в систему и появится кнопка логаута
        /// </summary>
        internal void WaitLoad()
        {
            Driver.FindElement(By.XPath("//a[@title='Logout']"));
        }
    }
}