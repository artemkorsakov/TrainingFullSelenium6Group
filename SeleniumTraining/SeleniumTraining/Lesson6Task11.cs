using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTraining.DriverHelper;
using SeleniumTraining.Entities;
using SeleniumTraining.Pages;

namespace SeleniumTraining
{
    [TestFixture]
    public class Lesson6Task11
    {
        private IWebDriver _driver;

        [SetUp]
        public void Start()
        {
            _driver = DriverFactory.CreateWebDriver(BrowserKind.Chrome);
        }

        /// <summary>
        /// Cценарий для регистрации нового пользователя в учебном приложении litecart
        /// </summary>
        [Test]
        public void CheckRegistration()
        {
            // Отключаем капчу в админке на вкладке Settings -> Security.
            AdminPage adminPage = new AdminPage(_driver);
            adminPage.Open();
            adminPage.Login("admin", "admin");
            adminPage.ClickMenu("Settings");
            adminPage.ClickSubmenu("Security");
            adminPage.EditKey("CAPTCHA");
            adminPage.SetBooleanKey("CAPTCHA", false);
            adminPage.SaveKey("CAPTCHA");
            adminPage.Logout();

            MainPage mainPage = new MainPage(_driver);
            mainPage.Open();
            mainPage.ClickRegistration();

            // Регистрация новой учётной записи с достаточно уникальным адресом электронной почты, а затем выход
            CreateAccountPage createAccountPage = new CreateAccountPage(_driver);
            User user = User.CreateRandomUser();
            createAccountPage.FillUser(user);
            createAccountPage.CreateAccount();
            mainPage.Logout();

            // Повторный вход в только что созданную учётную запись, и ещё раз выход
            mainPage.Login(user.Email, user.Password);
            mainPage.Logout();
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}