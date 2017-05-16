using OpenQA.Selenium;

namespace SeleniumTraining.Pages.AdminPages
{
    /// <summary>
    /// Страница редактирования продукта
    /// </summary>
    internal class EditProductPage : AdminPage
    {
        internal EditProductPage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Ожидание загрузки страницы
        /// </summary>
        internal new void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("form[name=product_form]"));
        }
    }
}