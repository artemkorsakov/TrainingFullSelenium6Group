using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Entities;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Страница Корзины с покупками
    /// </summary>
    internal class ShoppingCartPage
    {
        protected readonly IWebDriver Driver;

        internal ShoppingCartPage(IWebDriver driver)
        {
            Driver = driver;
        }

        /// <summary>
        /// Удаление заданного продукта из корзины
        /// </summary>
        internal void DeleteProduct(SimpleProduct product)
        {
            var removeButton =
                Driver.FindElement(
                    By.XPath($"//table[contains(@class, 'items')]//tr[.//a[.='{product.Name}']]//button[@title='Remove']"));
            removeButton.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(DriverFactory.TimeOutSeconds));
            wait.Until(ExpectedConditions.StalenessOf(removeButton));

            WaitLoad();
        }

        /// <summary>
        /// Ожидание загрузки страницы
        /// </summary>
        internal void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("form[name=checkout_form]"));
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(DriverFactory.TimeOutSeconds));
            wait.Until(ExpectedConditions.StalenessOf(Driver.FindElement(By.CssSelector(".loader-wrapper"))));
        }
    }
}