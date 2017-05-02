using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining.Pages.AdminPages
{
    /// <summary>
    /// Страница настроек
    /// </summary>
    internal class SettingsPage : AdminPage
    {
        internal SettingsPage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Редактировать заданный параметр
        /// </summary>
        internal void EditKey(string key)
        {
            Driver.FindElement(By.XPath($"//tr[td[.='{key}']]//a[@title='Edit']")).Click();
            // Ожидаем, когда появится кнопка "Сохранить" - так мы поймем, что страница перестроилась для редактирования
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//button[@value='Save']"));
        }

        /// <summary>
        /// Заданному параметру установить заданное булевское значение
        /// </summary>
        internal void SetBooleanKey(string key, bool value)
        {
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//label[normalize-space(.)='{value}']")).Click();
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//label[normalize-space(.)='{value}'][contains(@class, 'active')]"));
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        internal void SaveKey(string key)
        {
            Driver.FindElement(By.XPath($"//tr[td[starts-with((.), '{key}')]]//button[@value='Save']")).Click();

            // Надо дождаться, что сохранение прошло успешно
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div#notices > div.alert-success")));
        }

        /// <summary>
        /// Ожидание загрузки страницы
        /// </summary>
        internal new void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("form[name=settings_form]"));
        }
    }
}