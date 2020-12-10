using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSysteem
{
    /// <summary>
    /// This class represents the register.
    /// </summary>
    public class Register
    {
        private List<PaymentMethod> allowedCards;

        private List<Sale> sales;

        private List<Receipt> receipts;

        private double moneyAmountRegister;

        private Stock stock;

        private Receipt currentReceipt;

        /// <summary>
        /// Creates a new Register.
        /// </summary>
        /// <param name="startamount">amount of cash to start with.</param>
        public Register(double startamount)
        {
            this.allowedCards = new List<PaymentMethod>();
            this.sales = new List<Sale>();
            this.receipts = new List<Receipt>();
            this.stock = new Stock();
            this.moneyAmountRegister = startamount;
        }

        /// <summary>
        /// Gets a product by Id.
        /// </summary>
        /// <param name="id">Id string.</param>
        /// <returns>A Product.</returns>
        public Product GetProduct(string id)
        {
            return null; // TODO return Product class from stock (add method to stock?).
        }

        /// <summary>
        /// Checks stock.
        /// </summary>
        /// <param name="product">Product to check.</param>
        public bool CheckStock(Product product)
        {
            // TODO check stock (add method to stock?)
            return false;
        }

        /// <summary>
        /// Pays the current receipt.
        /// </summary>
        /// <param name="method">Method to pay with.</param>
        /// <param name="amount">Amount of money paid.</param>
        public bool Pay(PaymentMethod method, double amount)
        {
            return true; // TODO actually pay.
        }

        /// <summary>
        /// Adds a product to a receipt.
        /// </summary>
        /// <param name="product">Product to add.</param>
        public void AddToReceipt(Product product)
        {

        }

        /// <summary>
        /// Prints a receipt.
        /// </summary>
        public void PrintReceipt()
        {
            // TODO print receipt from this.currentReceipt;
            Console.WriteLine($" ----------------------");
            Console.WriteLine($"Date: {DateTime.Now}");
            Console.WriteLine($" ---------------------- ");
        }

        /// <summary>
        /// Adds a sale to this Register.
        /// </summary>
        /// <param name="sale"></param>
        public void AddSale(Sale sale)
        {
            this.sales.Add(sale);
        }

        /// <summary>
        /// Removes a sale from this Register.
        /// </summary>
        /// <param name="sale"></param>
        public void RemoveSale(Sale sale)
        {
            this.sales.Remove(sale);
        }

        /// <summary>
        /// Gets subtotal.
        /// </summary>
        /// <returns>Subtotal price.</returns>
        public double GetSubTotal()
        {
            return this.currentReceipt.GetPriceTotal();
        }

        /// <summary>
        /// Refunds a receipt.
        /// </summary>
        /// <param name="receipt">Receipt to refund.</param>
        /// <param name="product">Product to refund.</param>
        public double Refund(Receipt receipt, Product product)
        {
            // Remove product from receipt, return amount of money to give back.
            receipt.RemoveProduct(product);
            return product.GetPrice();
        }

        /// <summary>
        /// Gets profit margin.
        /// </summary>
        /// <returns>Profit margin.</returns>
        public double GetProfitMargin()
        {
            // TODO
            return 0;
        }

        /// <summary>
        /// Prints all sold products.
        /// </summary>
        public void PrintAllSoldProductFromReceipts()
        {
            //TODO yada yada
        }

        /// <summary>
        /// Gets total amount in register.
        /// </summary>
        /// <returns>Cash in register.</returns>
        public double GetRegisterAmount()
        {
            return this.moneyAmountRegister;
        }

        /// <summary>
        /// Sets the current amoun t of money available in the register.
        /// </summary>
        /// <param name="value"></param>
        public void SetRegisterAmount(double value)
        {
            this.moneyAmountRegister = value;
        }
    }
}
