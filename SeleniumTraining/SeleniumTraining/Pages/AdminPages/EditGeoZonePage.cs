using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTraining.Pages.AdminPages
{
    /// <summary>
    /// Страница редактирования геозоны
    /// </summary>
    internal class EditGeoZonePage : AdminPage
    {
        internal EditGeoZonePage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Проверяем, что зоны расположены в алфавитном порядке
        /// </summary>
        internal void AssertAlphabetZones()
        {
            var zonesName = Driver.FindElements(By.XPath("//form[@name='form_geo_zone']//td[input[contains(@name, '[zone_code]')]]"));
            string previousName = null;
            foreach (var zone in zonesName)
            {
                string currentName = zone.Text;
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
            Driver.FindElement(By.CssSelector("[name=form_geo_zone]"));
        }
    }
}