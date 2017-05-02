using System;

namespace SeleniumTraining.Entities
{
    /// <summary>
    /// Пользователь программы litecart
    /// </summary>
    internal class User
    {
        internal string Firstname;
        internal string Lastname;
        internal string TaxId;
        internal string Company;
        internal string Address1;
        internal string Address2;
        internal string Postcode;
        internal string City;
        internal string Country;
        internal string Zone;
        internal string Email;
        internal string Phone;
        internal string Password;
        internal bool IsSubscribe;

        /// <summary>
        /// Конструктор пользователя только с обязательными полями
        /// </summary>
        internal User(string firstname, string lastname, string taxId, string company,
            string address1, string address2, string postcode, string city, string country, string zone,
            string email, string phone, string password, bool isSubscribe)
        {
            Firstname = firstname;
            Lastname = lastname;
            TaxId = taxId;
            Company = company;
            Address1 = address1;
            Address2 = address2;
            Postcode = postcode;
            City = city;
            Country = country;
            Zone = zone;
            Email = email;
            Phone = phone;
            Password = password;
            IsSubscribe = isSubscribe;
        }

        /// <summary>
        /// Создать произвольного пользователя
        /// </summary>
        /// <returns></returns>
        internal static User CreateRandomUser()
        {
            int uniqueNumber = Rand.Next(1000, 9999);
            string firstname = $"Artyom{uniqueNumber}";
            string lastname = $"Korsakov{uniqueNumber}";
            string taxId = uniqueNumber.ToString();
            string company = "Action";
            string address1 = "Russia, Moscow, st. First, 1, 1";
            string address2 = "Russia, St. Petersburg, st. Second, 2, 2";
            string postcode = "12345";
            string city = "New York";
            string country = "United States";
            string zone = "New York";
            string email = $"user{uniqueNumber}@mail.ru";
            string phone = "+79269260000";
            string password = email;

            return new User(firstname, lastname, taxId, company, address1, address2, postcode, city, country, zone, email, phone, password, true);
        }

        // Статичный, чтобы при параллельном запуске не сгенерилось два одинаковых номера
        private static readonly Random Rand = new Random();
    }
}