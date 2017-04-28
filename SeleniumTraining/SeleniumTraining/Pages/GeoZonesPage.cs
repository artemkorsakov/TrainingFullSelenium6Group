using System.Linq;
using OpenQA.Selenium;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Страница гео зон
    /// </summary>
    internal class GeoZonesPage : AdminPage
    {
        internal GeoZonesPage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Вернуть список стран на странице
        /// </summary>
        internal string[] GetCountries()
        {
            var countries = Driver.FindElements(By.XPath("//form[@name='geo_zones_form']//a[not(@title='Edit')]"));
            var countriesName = countries.Select(country => country.Text).ToArray();
            return countriesName;
        }

        /// <summary>
        /// Перейти в заданную страну
        /// </summary>
        internal void ClickCountry(string name)
        {
            Driver.FindElement(By.XPath($"//a[not(@title='Edit')][.='{name}']")).Click();
            //Ждем загрузки страницы редактирования компании
            Driver.FindElement(By.CssSelector("[name=form_geo_zone]"));
        }

        /// <summary>
        /// Ожидание загрузки страницы:
        /// Ждем, когда появится форма геозон
        /// </summary>
        internal new void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("[name=geo_zones_form]"));
        }
    }
}