using System;
using System.Globalization;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Entities;

namespace SeleniumTraining.Pages.AdminPages
{
    /// <summary>
    /// Страница добавления нового продукта
    /// </summary>
    internal class AddNewProductPage : AdminPage
    {
        internal AddNewProductPage(IWebDriver driver) : base(driver)
        {
            WaitLoad();
        }

        /// <summary>
        /// Переход на закладку "General"
        /// </summary>
        internal void ClickTabMenu(string menu)
        {
            Driver.FindElement(By.XPath($"//form[@name='product_form']//a[.='{menu}']")).Click();

            // Надо дождаться, когда пункт меню станет активным
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(DriverFactory.TimeOutSeconds));
            wait.Until(
                ExpectedConditions.ElementExists(
                    By.XPath($"//form[@name='product_form']//li[@class='active']/a[.='{menu}']")));
        }

        /// <summary>
        /// Заполнение закладки "General"
        /// </summary>
        internal void FillGeneral(Product product)
        {
            // Status
            string status = product.Status ? "Enabled" : "Disabled";
            string locator = $"//form[@name='product_form']//label[input[@name='status']][normalize-space(.)='{status}']";
            Driver.FindElement(By.XPath(locator)).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(DriverFactory.TimeOutSeconds));
            wait.Until(ExpectedConditions.ElementExists(By.XPath($"{locator}[contains(@class, 'active')]")));

            // Code Name SKU GTIN TARIC
            Driver.FindElement(By.CssSelector("[name=product_form] [name=code]")).SendKeys(product.Code);
            Driver.FindElement(By.XPath("//form[@name='product_form']//input[contains(@name, 'name')]"))
                .SendKeys(product.Name);
            Driver.FindElement(By.CssSelector("[name=product_form] [name=sku]")).SendKeys(product.SKU);
            Driver.FindElement(By.CssSelector("[name=product_form] [name=gtin]")).SendKeys(product.GTIN);
            Driver.FindElement(By.CssSelector("[name=product_form] [name=taric]")).SendKeys(product.TARIC);

            // Images. Приложение запускается из папки bin/Debug
            try
            {
                DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
                string fullpath = directory.FullName + product.ImageDirectory;
                Driver.FindElement(By.XPath("//form[@name='product_form']//input[contains(@name, 'new_images')]")).SendKeys(fullpath);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Не удалось определить директорию текущего приложения");
            }

            // Quantity Weight Width Height Length
            SetValue("quantity", product.Quantity);
            SetValue("weight", product.Weight);
            SetValue("dim_x", product.Width);
            SetValue("dim_y", product.Height);
            SetValue("dim_z", product.Length);

            // DeliveryStatus SoldOutStatus
            new SelectElement(Driver.FindElement(By.CssSelector("[name=product_form] [name=delivery_status_id]"))).SelectByText(product.DeliveryStatus);
            new SelectElement(Driver.FindElement(By.CssSelector("[name=product_form] [name=sold_out_status_id]"))).SelectByText(product.SoldOutStatus);

            // Чекбокс Gender
            var checkbox = Driver.FindElement(By.XPath($"//form[@name='product_form']//label[normalize-space(.)='{product.Gender}']/input"));
            if (!checkbox.Selected)
            {
                checkbox.Click();
            }

            // DateValidFrom
            Driver.FindElement(By.CssSelector("[name=product_form] [name=date_valid_from]")).SendKeys(Keys.Home + product.DateValidFrom.ToString("MMddyyyy"));

            // DateValidTo
            Driver.FindElement(By.CssSelector("[name=product_form] [name=date_valid_to]")).SendKeys(product.DateValidTo.ToString("MMddyyyy"));
        }

        /// <summary>
        /// Заполнение закладки "Information"
        /// </summary>
        internal void FillInformation(Product product)
        {
            new SelectElement(Driver.FindElement(By.CssSelector("[name=product_form] [name=manufacturer_id]"))).SelectByText(product.Manufacturer);
            Driver.FindElement(By.CssSelector("[name=product_form] [name=keywords]")).SendKeys(product.Keywords);
            Driver.FindElement(By.XPath("//form[@name='product_form']//input[contains(@name, 'short_description')]")).SendKeys(product.ShortDescription);
            Driver.FindElement(By.XPath("//form[@name='product_form']//div[@class = 'trumbowyg-editor']")).SendKeys(product.Description);
            Driver.FindElement(By.XPath("//form[@name='product_form']//input[contains(@name, 'head_title')]")).SendKeys(product.HeadTitle);
            Driver.FindElement(By.XPath("//form[@name='product_form']//input[contains(@name, 'meta_description')]")).SendKeys(product.MetaDescription);
        }

        /// <summary>
        /// Заполнение закладки "Prices"
        /// </summary>
        internal void FillPrices(Product product)
        {
            SetValue("purchase_price", product.RegularPrice);
            new SelectElement(Driver.FindElement(By.CssSelector("[name=product_form] [name=purchase_price_currency_code]"))).SelectByText("US Dollars");
            SetValue("prices[USD]", product.PriceUsd);
            SetValue("prices[EUR]", product.PriceEuro);
            SetValue("gross_prices[USD]", product.PriceInclTaxUsd);
            SetValue("gross_prices[EUR]", product.PriceInclTaxEuro);
        }

        /// <summary>
        /// Сохранение страницы
        /// </summary>
        internal void Save()
        {
            Driver.FindElement(By.CssSelector("form[name=product_form] button[name=save]")).Click();
            new CatalogPage(Driver).WaitLoad();
        }

        /// <summary>
        /// Ожидание загрузки страницы
        /// </summary>
        internal new void WaitLoad()
        {
            Driver.FindElement(By.CssSelector("form[name=product_form]"));
        }

        /// <summary>
        /// Установить дробное значение в поле формы продукта, в котором уже стоит какое-то начальное значение
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        private void SetValue(string fieldName, double value)
        {
            var element = Driver.FindElement(By.XPath($"//form[@name='product_form']//input[contains(@name, '{fieldName}')]"));
            element.Clear();
            element.SendKeys(value.ToString("##0.#", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Установить дробное значение в поле формы продукта, в котором уже стоит какое-то начальное значение
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        private void SetValue(string fieldName, string value)
        {
            var element = Driver.FindElement(By.XPath($"//form[@name='product_form']//input[contains(@name, '{fieldName}')]"));
            element.Clear();
            element.SendKeys(value);
        }
    }
}