namespace SeleniumTraining.Entities
{
    /// <summary>
    /// Класс описывает продукт
    /// </summary>
    internal class Product
    {
        internal string Name { get; }
        internal string Manufacturer { get; }
        internal string RegularPrice { get; }
        internal string CampaignPrice { get; }

        internal Product(string name, string manufacturer, string regularPrice, string campaignPrice)
        {
            Name = name;
            Manufacturer = manufacturer;
            RegularPrice = regularPrice;
            CampaignPrice = campaignPrice;
        }

        /// <summary>
        /// Одинаковые ли продукты?
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool Equals(Product product)
        {
            return (this.Name == product.Name)
                && (this.Manufacturer == product.Manufacturer)
                && (this.RegularPrice == product.RegularPrice)
                && (this.CampaignPrice == product.CampaignPrice);
        }
    }
}