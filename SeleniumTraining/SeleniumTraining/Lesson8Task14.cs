using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages.AdminPages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson8Task14
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
        }

        /// <summary>
        /// Проверка открытия ссылок при создании новой страны
        /// 1) зайти в админку
        /// 2) открыть пункт меню Countries (или страницу http://localhost/litecart/admin/?app=countries&amp;doc=countries)
        /// 3) открыть на редактирование какую-нибудь страну или начать создание новой
        /// 4) возле некоторых полей есть ссылки с иконкой в виде квадратика со стрелкой -- они ведут на внешние страницы и открываются в новом окне, именно это и нужно проверить.
        /// </summary>
        [Test]
        public void CheckCountriesLinks()
        {
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Open();
            adminPage.Login("admin", "admin");
            adminPage.ClickMenu("Countries");
            CountriesPage countriesPage = new CountriesPage(_driver);
            countriesPage.ClickCountry("Russian Federation");
            EditCountryPage editCountryPage = new EditCountryPage(_driver);
            editCountryPage.AssertExternalLink();
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}