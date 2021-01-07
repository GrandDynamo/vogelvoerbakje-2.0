using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KassaSysteem
{
    public class Stock
    {
        private Dictionary<Product, int> productsInStock = new Dictionary<Product, int>();

        public void AddItem(Product product)
        {
            this.productsInStock.Add(product, 1);
        }

        public void RemoveItem(Product product)
        {
            if(this.productsInStock.ContainsKey(product))
                this.productsInStock[product]--;
        }

        public int GetItemAmount(Product product)
        {
            return this.productsInStock.ContainsKey(product) ? this.productsInStock[product] : 0;
        }

        public Product GetProduct(int id)
        {
            if(this.productsInStock.Keys.Any(x => x.GetProductId() == id))
                return this.productsInStock.Keys.First(x => x.GetProductId() == id);
            return null;
        }
    }
}
