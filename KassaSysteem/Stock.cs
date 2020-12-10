using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSysteem
{
    public class Stock
    {
        private Dictionary<Product, int> productsInStock = new Dictionary<Product, int>();

        public void AddItem(Product product)
        {
            productsInStock.Add(product, 1);
        }

        public void RemoveItem(Product product)
        {
    
        }
    }
}
