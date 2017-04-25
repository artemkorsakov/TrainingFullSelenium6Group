using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;

namespace SeleniumTraining.DriverHelper
{
    /// <summary>
    /// Фабрика по созданию браузера
    /// </summary>
    internal class DriverFactory
    {
        internal const int TimeOutSeconds = 5;

        /// <summary>
        /// Создание драйвера
        /// </summary>
        /// <returns></returns>
        internal static IWebDriver CreateWebDriver(BrowserKind kind)
        {
            IWebDriver driver;
            switch (kind)
            {
                case BrowserKind.InternetExplorer:
                    driver = CreateIEDriver();
                    break;
                case BrowserKind.Firefox:
                    driver = CreateFirefoxDriver();
                    break;
                case BrowserKind.FirefoxNightly:
                    driver = CreateFirefoxNightlyDriver();
                    break;
                case BrowserKind.VeryOldFirefox:
                    driver = CreateVeryOldFirefoxDriver();
                    break;
                case BrowserKind.Chrome:
                    driver = CreateChromeDriver();
                    break;
                case BrowserKind.PhantomJS:
                    driver = CreatePhantomJsDriver();
                    break;
                default:
                    driver = CreatePhantomJsDriver();
                    break;
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, TimeOutSeconds);

            return driver;
        }

        /// <summary>
        /// Создание драйвера InternetExplorer
        /// </summary>
        private static IWebDriver CreateIEDriver()
        {
            InternetExplorerOptions ieOptions = new InternetExplorerOptions();
            IWebDriver driver = new InternetExplorerDriver(ieOptions);
            return driver;
        }

        /// <summary>
        /// Создание драйвера Firefox (версия после 48-й) с использованием geckodriver
        /// Находится в папке:
        /// options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
        /// </summary>
        private static IWebDriver CreateFirefoxDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = false;
            IWebDriver driver = new FirefoxDriver(options);
            return driver;
        }

        /// <summary>
        /// Создание драйвера FirefoxNightly с использованием geckodriver
        /// Находится в папке:
        /// options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
        /// </summary>
        private static IWebDriver CreateFirefoxNightlyDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = false;
            options.BrowserExecutableLocation = @"C:\Program Files (x86)\Nightly\firefox.exe";
            IWebDriver driver = new FirefoxDriver(options);
            return driver;
        }

        /// <summary>
        /// Создание старого драйвера Firefox до 47 версии браузера включительно
        /// Находится в папке:
        /// options.BrowserExecutableLocation = @"C:\SeleniumTraining\Mozilla Firefox 45\firefox.exe";
        /// </summary>
        private static IWebDriver CreateVeryOldFirefoxDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"C:\SeleniumTraining\Mozilla Firefox 45\firefox.exe";
            IWebDriver driver = new FirefoxDriver(options);
            return driver;
        }

        /// <summary>
        /// Создание драйвера Chrome
        /// </summary>
        private static IWebDriver CreateChromeDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(chromeOptions);
            return driver;
        }

        /// <summary>
        /// Создание драйвера PhantomJs
        /// </summary>
        private static IWebDriver CreatePhantomJsDriver()
        {
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
            IWebDriver driver = new PhantomJSDriver(service);
            return driver;
        }
    }
}