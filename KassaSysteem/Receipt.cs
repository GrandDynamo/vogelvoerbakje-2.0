using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KassaSysteem
{
    /// <summary>
    /// Represents a Receipt.
    /// </summary>
    public class Receipt
    {
        [JsonProperty]
        private Dictionary<Product, int> productOnReceipt = new Dictionary<Product, int>();

        [JsonProperty]
        private PaymentMethod usedPaymentMethod = PaymentMethod.Cash;

        /// <summary>
        /// Constructs a new receipt.
        /// </summary>
        public Receipt()
        {
        }

        //TODO change
        /// <summary>
        /// Adds a product to the dictionary
        /// </summary>
        public void AddProduct(Product product)
        {
            if (productOnReceipt.ContainsKey(product))
                this.productOnReceipt[product]++;
            else
                this.productOnReceipt.Add(product, 1);
        }

        /// <summary>
        /// Removes a product from the dictionary
        /// </summary>
        public void RemoveProduct(Product product)
        {
            if (productOnReceipt.ContainsKey(product))
            {
                this.productOnReceipt[product]--;
                if (productOnReceipt[product] < 1)
                    this.productOnReceipt.Remove(product);
            }
        }

        //TODO change + calculate sales
        /// <summary>
        /// Calculates price based on the items inside the dictionary
        /// </summary>
        public double GetPriceTotal()
        {
            double PriceFromDictionary = 0.0;
            return PriceFromDictionary;
        }

        /// <summary>
        /// Sets payment method.
        /// </summary>
        /// <param name="method">Payment method to use.</param>
        public void SetPaymentMethod(PaymentMethod method)
        {
            this.usedPaymentMethod = method;
        }

        /// <summary>
        /// Gets payment method.
        /// </summary>
        /// <returns>Payment method that is used.</returns>
        public PaymentMethod GetPaymentMethod()
        {
            return this.usedPaymentMethod;
        }

        /// <summary>
        /// Gets products.
        /// </summary>
        /// <returns>Disctionary with Products as keys and amounts as values.</returns>
        public Dictionary<Product, int> GetProducts()
        {
            return this.productOnReceipt;
        }
    }
}
