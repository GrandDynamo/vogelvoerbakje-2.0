using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSysteem
{
    public class Receipt
    {
        private Dictionary<Product, int> productOnReceipt = new Dictionary<Product, int>();
        private PaymentMethod usedPaymentMethod;

        public Receipt(PaymentMethod usedPaymentMethod)
        {
            this.usedPaymentMethod = usedPaymentMethod;
        }

        //TODO change
        /// <summary>
        /// Adds a product to the dictionary
        /// </summary>
        public void AddProduct(Product product)
        {

        }

        //TODO change
        /// <summary>
        /// Removes a product from the dictionary
        /// </summary>
        public void RemoveProduct(Product product)
        {

        }

        //TODO change
        /// <summary>
        /// Calculates price based on the items inside the dictionary
        /// </summary>
        public double GetPriceTotal()
        {
            double PriceFromDictionary = 0.0;
            return PriceFromDictionary;
        }

        public PaymentMethod GetPaymentMethod()
        {
            return this.usedPaymentMethod;
        }
    }
}
