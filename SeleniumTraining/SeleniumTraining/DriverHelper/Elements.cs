using System;
using OpenQA.Selenium;

namespace SeleniumTraining.DriverHelper
{
    internal static class Elements
    {
        /// <summary>
        /// Существует ли в данный момент времени элемент с заданным локатором
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        internal static bool IsElementPresent(IWebDriver driver, By locator)
        {
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0);
            try
            {
                driver.FindElement(locator);
                driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, DriverFactory.TimeOutSeconds);
                return true;
            }
            catch (NoSuchElementException)
            {
                driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, DriverFactory.TimeOutSeconds);
                return false;
            }
        }

        /// <summary>
        /// Существуют ли в данный момент времени элементы с заданным локатором
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        internal static bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }
    }
}