using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Entities;
using SeleniumTraining.Pages.AdminPages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson6Task12
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
        }

        /// <summary>
        /// Сценарий для добавления нового товара(продукта) в учебном приложении litecart(в админке).
        /// </summary>
        [Test]
        public void CheckAddNewProduct()
        {
            // Отключаем капчу в админке на вкладке Settings -> Security.
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Open();
            adminPage.Login("admin", "admin");
            adminPage.ClickMenu("Catalog");

            CatalogPage catalogPage = new CatalogPage(_driver);
            catalogPage.AddNewProduct();
            AddNewProductPage addNewProductPage = new AddNewProductPage(_driver);
            Product mallardDuck = Product.CreateMallardDuck();
            addNewProductPage.ClickTabMenu("General");
            addNewProductPage.FillGeneral(mallardDuck);
            addNewProductPage.ClickTabMenu("Information");
            addNewProductPage.FillInformation(mallardDuck);
            addNewProductPage.ClickTabMenu("Prices");
            addNewProductPage.FillPrices(mallardDuck);
            addNewProductPage.Save();

            // После сохранения товара нужно убедиться, что он появился в каталоге (в админке)
            catalogPage.AssertProduct(mallardDuck);
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}