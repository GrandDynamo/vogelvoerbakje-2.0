using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KassaSysteem
{
    public class Receipt
    {
        private Dictionary<Product, int> productOnReceipt = new Dictionary<Product, int>();
        private PaymentMethod usedPaymentMethod = PaymentMethod.Cash;

        public Receipt()
        {
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

        //TODO change + calculate sales
        /// <summary>
        /// Calculates price based on the items inside the dictionary
        /// </summary>
        public double GetPriceTotal()
        {
            double PriceFromDictionary = 0.0;
            return PriceFromDictionary;
        }

        public void SetPaymentMethod(PaymentMethod method)
        {
            this.usedPaymentMethod = method;
        }

        public PaymentMethod GetPaymentMethod()
        {
            return this.usedPaymentMethod;
        }

        public Dictionary<Product, int> GetProducts()
        {
            return this.productOnReceipt;
        }
    }
}
