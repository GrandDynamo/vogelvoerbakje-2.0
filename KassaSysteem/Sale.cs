using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSysteem
{
    /// <summary>
    /// Represents one price for a combination of products (instead of having their own prices).
    /// </summary>
    public class Sale
    {
        [JsonProperty]
        List<Product> productTypes = new List<Product>();

        [JsonProperty]
        private double PriceSale;

        public Sale(double PriceSale)
        {
            this.PriceSale = PriceSale;
        }

        public double GetPriceSale()
        {
            return this.PriceSale;
        }

        public void AddProductToSale(Product product)
        {
            productTypes.Add(product);
        }

        public void DeleteProductFromSale(Product product)
        {
            productTypes.Remove(product);
        }

        public void GetAllItemFromList()
        {
            foreach(Product item in productTypes)
            {
                Console.WriteLine(item.GetProductName());
            }
        }
    }
}
