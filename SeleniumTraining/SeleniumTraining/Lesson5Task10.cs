using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Entities;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson5Task10
    {
        private IWebDriver _driver;

        /// <summary>
        /// Сценарий:
        /// Сделайте сценарий, который проверяет, что при клике на товар открывается правильная страница товара в учебном приложении litecart.
        /// Более точно, нужно открыть главную страницу, выбрать первый товар в категории Campaigns и проверить следующее:
        /// а) на главной странице и на странице товара совпадает текст названия товара
        /// б) на главной странице и на странице товара совпадают цены(обычная и акционная)
        /// в) обычная цена зачёркнутая и серая(можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
        /// г) акционная жирная и красная(можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
        /// (цвета надо проверить на каждой странице независимо, при этом цвета на разных страницах могут не совпадать)
        /// г) акционная цена крупнее, чем обычная(это тоже надо проверить на каждой странице независимо)
        /// Необходимо убедиться, что тесты работают в разных браузерах, желательно проверить во всех трёх ключевых браузерах(Chrome, Firefox, IE).
        /// </summary>
        [Test]
        public void CheckCountries()
        {
            BrowserKind[] browsers = { BrowserKind.Chrome, BrowserKind.Firefox, BrowserKind.IE };
            foreach (var browser in browsers)
            {
                _driver = DriverFactory.CreateWebDriver(browser);
                MainPage mainPage = new MainPage(_driver);
                mainPage.Open();
                SimpleProduct productMainPage = mainPage.GetFirstProductAndCheckIt();
                mainPage.ClickFirstProduct();
                SimpleProduct product = mainPage.GetSelectedProductAndCheckIt();
                Assert.True(productMainPage.Equals(product));
                _driver.Quit();
                _driver = null;
            }
        }

        /// <summary>
        /// На случай аварийного завершения
        /// </summary>
        [TearDown]
        public void Stop()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}