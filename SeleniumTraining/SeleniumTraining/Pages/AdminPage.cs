using OpenQA.Selenium;

namespace SeleniumTraining.Pages
{
    /// <summary>
    /// Страница администратора
    /// </summary>
    internal class AdminPage
    {
        private readonly IWebDriver _driver;

        internal AdminPage(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Вход на панель администратора под заданными логином и паролем
        /// </summary>
        internal void Login(string login, string password)
        {
            _driver.FindElement(By.Name("username")).SendKeys(login);
            _driver.FindElement(By.Name("password")).SendKeys(password);
            _driver.FindElement(By.XPath("//button[.='Login']")).Click();
        }

        /// <summary>
        /// Разлогирование с панели администратора
        /// </summary>
        internal void Logout()
        {
            _driver.FindElement(By.XPath("//a[@title='Logout']")).Click();
        }
    }
}