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

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
        }

        [Test]
        public void CheckRepository()
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