using System;
using OpenQA.Selenium;

namespace SeleniumTraining.DriverHelper
{
    internal static class ScreenShotHelper
    {
        /// <summary>
        /// Снятие скриншота
        /// </summary>
        /// <returns></returns>
        internal static void SaveScreenshot(IWebDriver driver)
        {
            string fullpath = $"C:\\SeleniumTraining\\Logs\\screen-{DateTime.Today.ToString("dd.MM.yyyy.HH.mm.ss")}.png";
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fullpath, ScreenshotImageFormat.Png);
        }
    }
}