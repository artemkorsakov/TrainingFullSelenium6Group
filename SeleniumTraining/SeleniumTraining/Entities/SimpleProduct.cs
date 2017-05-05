namespace SeleniumTraining.Entities
{
    /// <summary>
    /// Класс описывает простой продукт
    /// </summary>
    internal class SimpleProduct
    {
        internal string Name { get; }
        internal string Manufacturer { get; }
        internal string RegularPrice { get; }
        internal string CampaignPrice { get; }

        internal SimpleProduct(string name, string manufacturer)
        {
            Name = name;
            Manufacturer = manufacturer;
            RegularPrice = "unknown";
            CampaignPrice = "unknown";
        }

        internal SimpleProduct(string name, string manufacturer, string regularPrice, string campaignPrice)
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
        public bool Equals(SimpleProduct product)
        {
            return (this.Name == product.Name)
                && (this.Manufacturer == product.Manufacturer)
                && (this.RegularPrice == product.RegularPrice)
                && (this.CampaignPrice == product.CampaignPrice);
        }
    }
}