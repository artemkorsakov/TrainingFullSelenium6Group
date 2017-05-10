using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;

namespace SeleniumTraining.DriverHelper
{
    /// <summary>
    /// Класс работы с окнами
    /// </summary>
    internal class WindowsHelper
    {
        /// <summary>
        /// Ожидает, когда появится какое-нибудь новое окно, и возвращает его идентификатор
        /// </summary>
        /// <returns></returns>
        internal static string GetWindowHandle(IWebDriver driver, ICollection<string> oldWindows)
        {
            var getTime = new Stopwatch();
            getTime.Start();

            while (getTime.Elapsed.TotalSeconds < DriverFactory.TimeOutSeconds)
            {
                ICollection<string> currentWindows = driver.WindowHandles;
                if (currentWindows.Any(currentWindow => !oldWindows.Contains(currentWindow)))
                {
                    return currentWindows.FirstOrDefault(currentWindow => !oldWindows.Contains(currentWindow));
                }
            }

            return null;
        }
    }
}