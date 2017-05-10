using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;

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
        /// Проверяем, что все ссылки с иконкой в виде квадратика со стрелкой ведут к открытию нового окна
        /// </summary>
        internal void AssertExternalLink()
        {
            var externalLinks = Driver.FindElements(By.CssSelector(".fa-external-link"));
            foreach (var externalLink in externalLinks)
            {
                string mainWindow = Driver.CurrentWindowHandle;
                ICollection<string> oldWindows = Driver.WindowHandles;
                externalLink.Click();
                string newWindow = WindowsHelper.GetWindowHandle(Driver, oldWindows);
                Driver.SwitchTo().Window(newWindow);
                Console.WriteLine($"Открылось новое окно с заголовком: \"{Driver.Title}\"");
                Driver.Close();
                Driver.SwitchTo().Window(mainWindow);
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