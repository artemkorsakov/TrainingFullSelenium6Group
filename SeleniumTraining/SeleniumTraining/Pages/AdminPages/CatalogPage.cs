using NUnit.Framework;
using OpenQA.Selenium;
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