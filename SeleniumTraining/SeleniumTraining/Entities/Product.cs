using System;

namespace SeleniumTraining.Entities
{
    /// <summary>
    /// Класс описывает продукт
    /// </summary>
    internal class Product : SimpleProduct
    {
        // General
        internal bool Status;
        internal string Code;
        internal string SKU;
        internal string GTIN;
        internal string TARIC;
        internal string ImageDirectory;
        internal int Quantity;
        internal double Weight;
        internal double Width;
        internal double Height;
        internal double Length;
        internal string DeliveryStatus;
        internal string SoldOutStatus;
        internal string Gender;
        internal DateTime DateValidFrom;
        internal DateTime DateValidTo;

        // Information
        internal string Keywords;
        internal string ShortDescription;
        internal string Description;
        internal string HeadTitle;
        internal string MetaDescription;

        // Prices
        internal double PriceUsd;
        internal double PriceEuro;
        internal double PriceInclTaxUsd;
        internal double PriceInclTaxEuro;

        internal Product(string name, string manufacturer, string regularPrice, string campaignPrice)
            : base(name, manufacturer, regularPrice, campaignPrice)
        {
        }

        /// <summary>
        /// Создание утки MallardDuck для продажи
        /// </summary>
        /// <returns></returns>
        internal static Product CreateMallardDuck()
        {
            int uniqueNumber = new Random().Next(1000, 9999);

            Product mallardDuck = new Product("Кряква", "ACME Corp.", "22.9012", "22.9012")
            {
                // General
                Status = true,
                Code = uniqueNumber.ToString(),
                SKU = $"SKU{uniqueNumber}",
                GTIN = $"GTIN{uniqueNumber}",
                TARIC = $"TARIC{uniqueNumber}",
                ImageDirectory = @"\Images\MallardDuck.jpg",
                Quantity = 100,
                Weight = 1.6,
                Width = 100.55,
                Height = 40.44,
                Length = 62.22,
                DeliveryStatus = "3-5 days",
                SoldOutStatus = "Temporary sold out",
                Gender = "Male",
                DateValidFrom = DateTime.Today,
                DateValidTo = DateTime.Today.AddMonths(3),

                // Information
                Keywords = "утка, кряква",
                ShortDescription = "птица из семейства утиных (Anatidae) отряда гусеобразных (Anseriformes)",
                Description = "Кря́ква (лат. Anas platyrhynchos) — птица из семейства утиных (Anatidae) отряда гусеобразных (Anseriformes). Наиболее известная и распространённая дикая утка. Длина тела самца около 62 см, самки — около 57 см, масса достигает 1—1,5 кг (осенью, когда птица откормится перед самым перелётом, её вес может достигать 2 кг). Голова и шея самца зелёные, зоб и грудь коричнево-бурые, спина и брюшная сторона тела серого цвета с тонкими поперечными пятнышками. Окраска самки бурая с более тёмными пятнышками, брюшная сторона буровато-серая с продольными пестринами. На крыле у самца и самки сине-фиолетовое «зеркало»",
                HeadTitle = "Кря́ква",
                MetaDescription = "Утка Кря́ква",

                // Prices
                PriceUsd = 22.9012,
                PriceEuro = 21.0335,
                PriceInclTaxUsd = 26.3233,
                PriceInclTaxEuro = 24.1764
            };
            return mallardDuck;
        }
    }
}