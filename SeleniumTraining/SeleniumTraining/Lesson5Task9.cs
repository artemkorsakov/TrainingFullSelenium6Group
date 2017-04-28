using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson5Task9
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
        }

        /// <summary>
        /// Сценарий:
        /// 1) на странице http://localhost/litecart/admin/?app=countries&amp;doc=countries
        /// а) проверить, что страны расположены в алфавитном порядке
        /// б) для тех стран, у которых количество зон отлично от нуля -- открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке
        /// </summary>
        [Test]
        public void CheckCountries()
        {
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Open();
            adminPage.Login("admin", "admin");
            adminPage.ClickMenu("Countries");
            CountriesPage countriesPage = new CountriesPage(_driver);
            countriesPage.AssertAlphabetOrder();
            var countriesName = countriesPage.GetCountriesWithZones();
            foreach (var name in countriesName)
            {
                countriesPage.ClickCountry(name);
                EditCountryPage editCountryPage = new EditCountryPage(_driver);
                editCountryPage.AssertAlphabetZones();
                editCountryPage.ClickMenu("Countries");
            }
        }

        /// <summary>
        /// Сценарий:
        /// 2) на странице http://localhost/litecart/admin/?app=geo_zones&amp;doc=geo_zones
        /// зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке
        /// </summary>
        [Test]
        public void CheckGeoZones()
        {
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Open();
            adminPage.Login("admin", "admin");
            adminPage.ClickMenu("Geo Zones");
            GeoZonesPage geoZonesPage = new GeoZonesPage(_driver);
            var countriesName = geoZonesPage.GetCountries();
            foreach (var name in countriesName)
            {
                geoZonesPage.ClickCountry(name);
                EditGeoZonePage editGeoZonePage = new EditGeoZonePage(_driver);
                editGeoZonePage.AssertAlphabetZones();
                editGeoZonePage.ClickMenu("Geo Zones");
            }
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}