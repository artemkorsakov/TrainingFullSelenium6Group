using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Entities;

namespace SeleniumTraining.Pages.AdminPages
{
    /// <summary>
    /// Страница каталогов и товаров
    /// </summary>
    internal class CatalogPage : AdminPage
    {
        internal CatalogPage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Открыть каталог товаров
        /// </summary>
        internal void OpenFolder(string name)
        {
            if (Elements.AreElementsPresent(Driver,
                By.XPath($"//strong[preceding-sibling::i[contains(@class, 'fa-folder-open')]]/a[.='{name}']")))
            {
                return; // Папочка уже открыта
            }

            Driver.FindElement(By.XPath($"//a[.='{name}'][preceding-sibling::i[contains(@class, 'fa-folder')]]")).Click();
            // Ждем, когда папочка раскроется
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(DriverFactory.TimeOutSeconds));
            wait.Until(ExpectedConditions.ElementExists(By.XPath($"//strong[preceding-sibling::i[contains(@class, 'fa-folder-open')]]/a[.='{name}']")));
        }

        /// <summary>
        /// Вернуть все продукты со страницы
        /// </summary>
        internal string[] GetAllIdsProduct()
        {
            var elements = Driver.FindElements(By.XPath("//tr/td/input[contains(@name, 'products')]"));
            return elements.Select(el => el.GetAttribute("name")).ToArray();
        }

        /// <summary>
        /// Выбрать продукт по идентификатору
        /// </summary>
        internal void SelectProductById(string id)
        {
            Driver.FindElement(By.XPath($"//tr[td/input[@name='{id}']]//a[not(@title)]")).Click();
            new EditProductPage(Driver).WaitLoad();
        }

        /// <summary>
        /// Добавить новый продукт
        /// </summary>
        internal void AddNewProduct()
        {
            Driver.FindElement(By.XPath("//a[normalize-space(.)='Add New Product']")).Click();
            new AddNewProductPage(Driver).WaitLoad();
        }

        /// <summary>
        /// Проверяет, что заданный продукт появился в списке
        /// </summary>
        internal void AssertProduct(Product product)
        {
            Assert.True(Elements.AreElementsPresent(Driver, By.XPath($"//form[@name='catalog_form']//a[.='{product.Name}']")));
        }

        /// <summary>
        /// Ожидание загрузки страницы
        /// </summary>
        internal new void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("form[name=catalog_form]"));
        }
    }
}