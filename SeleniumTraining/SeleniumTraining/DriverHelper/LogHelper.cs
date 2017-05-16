using System;
using OpenQA.Selenium;

namespace SeleniumTraining.DriverHelper
{
    internal static class LogHelper
    {
        /// <summary>
        /// Вывести в консоль логи браузера
        /// </summary>
        /// <returns></returns>
        internal static void GetLogEntry(IWebDriver driver)
        {
            foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
            {
                Console.WriteLine(l);
            }
        }
    }
}