using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.Entities;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Страница регистрации нового пользователя
    /// </summary>
    internal class CreateAccountPage
    {
        private readonly IWebDriver _driver;

        internal CreateAccountPage(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        internal void Open()
        {
            _driver.Url = "http://localhost:8080/litecart/en/create_account";
        }

        /// <summary>
        /// Заполнение данных пользователя
        /// </summary>
        internal void FillUser(User user)
        {
            _driver.FindElement(By.CssSelector("[name=tax_id]")).SendKeys(user.TaxId);
            _driver.FindElement(By.CssSelector("[name=company]")).SendKeys(user.Company);
            _driver.FindElement(By.CssSelector("[name=firstname]")).SendKeys(user.Firstname);
            _driver.FindElement(By.CssSelector("[name=lastname]")).SendKeys(user.Lastname);
            _driver.FindElement(By.CssSelector("[name=address1]")).SendKeys(user.Address1);
            _driver.FindElement(By.CssSelector("[name=address2]")).SendKeys(user.Address2);
            _driver.FindElement(By.CssSelector("[name=postcode]")).SendKeys(user.Postcode);
            _driver.FindElement(By.CssSelector("[name=city]")).SendKeys(user.City);
            new SelectElement(_driver.FindElement(By.CssSelector("[name=country_code]"))).SelectByText(user.Country);
            new SelectElement(_driver.FindElement(By.CssSelector("[name=zone_code]"))).SelectByText(user.Zone);
            _driver.FindElement(By.CssSelector("[name=customer_form] [name=email]")).SendKeys(user.Email);
            _driver.FindElement(By.CssSelector("[name=phone]")).SendKeys(user.Phone);
            _driver.FindElement(By.CssSelector("[name=customer_form] [name=password]")).SendKeys(user.Password);
            _driver.FindElement(By.CssSelector("[name=confirmed_password]")).SendKeys(user.Password);
            FillSubscribe(user.IsSubscribe);
        }

        /// <summary>
        /// Заполнение чекбокса подписки
        /// </summary>
        internal void FillSubscribe(bool isSubscribe)
        {
            var checkbox = _driver.FindElement(By.CssSelector("[name=newsletter]"));

            // Если нужно установить чекбокс, а он не установлен, то кликаем
            // Если нужно снять чекбокс, а он установлен, то кликаем
            if (isSubscribe != checkbox.Selected)
            {
                checkbox.Click();
            }
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        internal void CreateAccount()
        {
            _driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

            // Надо дождаться, что сохранение прошло успешно
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#box-account")));
        }
    }
}