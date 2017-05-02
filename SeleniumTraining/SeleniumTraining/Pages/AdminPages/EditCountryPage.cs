using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTraining.Pages.AdminPages
{
    /// <summary>
    /// Страница редактирования компании
    /// </summary>
    internal class EditCountryPage : AdminPage
    {
        internal EditCountryPage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Проверяем, что зоны расположены в алфавитном порядке
        /// </summary>
        internal void AssertAlphabetZones()
        {
            var zonesName = Driver.FindElements(By.XPath("//form[@name='country_form']//input[contains(@name, '[name]')]"));
            string previousName = null;
            foreach (var zone in zonesName)
            {
                string currentName = zone.GetAttribute("value");
                if (previousName != null)
                {
                    Assert.True(currentName.CompareTo(previousName) > 0);
                }

                previousName = currentName;
            }
        }

        /// <summary>
        /// Ожидание загрузки страницы:
        /// Ждем, когда появится форма редактирования страны
        /// </summary>
        internal new void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("form[name=country_form]"));
        }
    }
}