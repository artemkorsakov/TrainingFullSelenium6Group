using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTraining.Pages.AdminPages
{
    /// <summary>
    /// Страница стран
    /// </summary>
    internal class CountriesPage : AdminPage
    {
        internal CountriesPage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Проверяем, что страны расположены в алфавитном порядке
        /// </summary>
        internal void AssertAlphabetOrder()
        {
            var countriesName = Driver.FindElements(By.XPath("//form[@name='countries_form']//a[not(@title='Edit')]"));
            string previousName = null;
            foreach (var countryName in countriesName)
            {
                string currentName = countryName.Text;
                if (previousName != null)
                {
                    // Достаточно сравнить, что текущая страна расположена позже в алфавитном порядке, чем предыдущая.
                    // Если это верно для всех пар, то, очевидно, страны расположены в алфавитном порядке
                    Assert.True(currentName.CompareTo(previousName) > 0);
                }

                previousName = currentName;
            }
        }

        /// <summary>
        /// Возвращает список стран с ненулевым количеством зон
        /// </summary>
        internal string[] GetCountriesWithZones()
        {
            var countries = Driver.FindElements(By.XPath("//form[@name='countries_form']//tr[td[6][not(.='0')]]//a[not(@title='Edit')]"));
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
            Driver.FindElement(By.CssSelector("form[name=country_form]"));
        }

        /// <summary>
        /// Ожидание загрузки страницы:
        /// Ждем, когда появится форма стран
        /// </summary>
        internal new void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("form[name=countries_form]"));
        }
    }
}