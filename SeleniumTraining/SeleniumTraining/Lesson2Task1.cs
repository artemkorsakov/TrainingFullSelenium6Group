using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson2Task1
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            _driver.Url = "http://software-testing.ru/forum/";
            _driver.FindElement(By.Id("main_search")).SendKeys("Проверка поиска");
            _driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}