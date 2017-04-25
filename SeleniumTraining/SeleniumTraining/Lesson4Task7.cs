using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson4Task7
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
        }

        /// <summary>
        /// Сценарий:
        /// 1) входит в панель администратора http://localhost/litecart/admin
        /// 2) прокликивает последовательно все пункты меню слева, включая вложенные пункты
        /// 3) для каждой страницы проверяет наличие заголовка (то есть элемента с тегом h1)
        /// </summary>
        [Test]
        public void CheckMenuTitle()
        {
            _driver.Url = "http://localhost:8080/litecart/admin/";
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Login("admin", "admin");

            var menus = adminPage.GetMenusName();
            foreach (var menu in menus)
            {
                adminPage.ClickMenu(menu);
                var submenus = adminPage.GetSubmenusName(menu);
                foreach (var submenu in submenus)
                {
                    adminPage.ClickSubmenu(menu, submenu);
                    adminPage.AssertPresentTitle();
                }
            }
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}