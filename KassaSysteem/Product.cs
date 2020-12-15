using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSysteem
{
    /// <summary>
    /// Represents a Product.
    /// </summary>
    public class Product
    {
        [JsonProperty]
        private int productId;

        [JsonProperty]
        private string productName;

        [JsonProperty]
        private double price;

        [JsonProperty]
        private double sale;

        /// <summary>
        /// Constructs a new Product.
        /// </summary>
        /// <param name="productId">Id for this product.</param>
        /// <param name="productName">Name for this product.</param>
        /// <param name="price">Price for this product.</param>
        /// <param name="sale">Sale over this product.</param>
        public Product(int productId, string productName, double price, double sale)
        {
            this.productId = productId;
            this.productName = productName;
            this.price = price;
            this.sale = sale;
        }
            
        /// <summary>
        /// Retrieves id of the product.
        /// </summary>
        /// <returns></returns>
        public int GetProductId()
        {
            return this.productId;
        }

        /// <summary>
        /// Sets id of the product.
        /// </summary>
        /// <param name="id"></param>
        public void SetProductId(int id)
        {
            this.productId = id;
        }

        /// <summary>
        /// Retrieves name of the product.
        /// </summary>
        /// <returns></returns>
        public string GetProductName()
        {
            return this.productName;
        }

        /// <summary>
        /// Sets name of the product.
        /// </summary>
        /// <param name="name"></param>
        public void SetProductName(string name)
        {
            this.productName = name;
        }

        /// <summary>
        /// Gets price of the product.
        /// </summary>
        /// <returns></returns>
        public double GetPrice()
        {
            return this.price;
        }

        /// <summary>
        /// Sets price of product.
        /// </summary>
        /// <param name="price"></param>
        public void SetPrice(double price)
        {
            this.price = price;
        }

        /// <summary>
        /// Gets amount of sale of the product.
        /// </summary>
        /// <returns></returns>
        public double GetSale()
        {
            return this.sale;
        }

        /// <summary>
        /// Sets amount of sale for the product.
        /// </summary>
        /// <param name="saleAmount"></param>
        public void SetSale(double saleAmount)
        {
            this.sale = saleAmount;
        }
    }
}
