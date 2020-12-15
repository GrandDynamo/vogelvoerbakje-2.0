using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KassaSysteem
{
    /// <summary>
    /// This class represents the register.
    /// </summary>
    public class Register
    {
        [JsonProperty]
        private List<PaymentMethod> allowedCards;

        [JsonProperty]
        private List<Sale> sales;

        [JsonProperty]
        private List<Receipt> receipts;

        [JsonProperty]
        private double startAmountRegister;

        [JsonProperty]
        private double moneyAmountRegister;

        [JsonProperty]
        private Stock stock;

        [JsonIgnore]
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
            this.startAmountRegister = startamount;
            this.moneyAmountRegister = startamount;
            this.currentReceipt = new Receipt();
        }

        /// <summary>
        /// Gets a product by Id.
        /// </summary>
        /// <param name="id">Id string.</param>
        /// <returns>A Product.</returns>
        public Product GetProduct(int id)
        {
            return this.stock.GetProduct(id);
        }

        /// <summary>
        /// Checks stock.
        /// </summary>
        /// <param name="product">Product to check.</param>
        public bool CheckStock(Product product)
        {
            // stock > 0? is in stock
            return stock.GetItemAmount(product) > 0;
        }

        /// <summary>
        /// Pays the current receipt.
        /// </summary>
        /// <param name="method">Method to pay with.</param>
        /// <param name="amount">Amount of money paid.</param>
        public bool Pay(PaymentMethod method, double amount)
        {
            if (!this.allowedCards.Contains(method) || amount < this.GetSubTotal())
                return false;

            this.currentReceipt.SetPaymentMethod(method);
            this.PrintReceipt();
            this.receipts.Add(this.currentReceipt);

            this.currentReceipt = new Receipt();
            return true;
        }

        /// <summary>
        /// Adds a product to a receipt.
        /// </summary>
        /// <param name="product">Product to add.</param>
        public void AddToReceipt(Product product)
        {
            this.currentReceipt.AddProduct(product);
        }

        /// <summary>
        /// Prints a receipt.
        /// </summary>
        public void PrintReceipt()
        {
            // TODO print receipt from this.currentReceipt;
            Console.WriteLine($" ----------------------");
            Console.WriteLine($"Date: {DateTime.Now}\n");
            foreach(var keyvalue in currentReceipt.GetProducts())
            {
                for(int i = 0; i < keyvalue.Value; i++)
                {
                    Console.WriteLine($"{keyvalue.Key.GetProductName()}: {keyvalue.Key.GetPrice()}");
                }
            }
            Console.WriteLine($"\n\nTotal: {currentReceipt.GetPriceTotal()}");
            Console.WriteLine($"Payment method: {Enum.GetName(typeof(PaymentMethod), currentReceipt.GetPaymentMethod())}");
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
            double profitMargin = moneyAmountRegister - startAmountRegister;

            return profitMargin;
        }

        /// <summary>
        /// Prints all sold products.
        /// </summary>
        public void PrintAllSoldProductFromReceipts()
        {
            //TODO
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

        /// <summary>
        /// Serializes this register to disk. (register.json)
        /// </summary>
        public void SerializeToDisk()
        {
            if (!File.Exists("register.json"))
                File.Create("register.json").Close();

            File.WriteAllText("register.json", JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        /// <summary>
        /// Deserializes a register from disk (and creates a new one if it doesn't exist). (register.json)
        /// </summary>
        /// <returns></returns>
        public static Register DeserializeFromDisk()
        {
            if (!File.Exists("register.json"))
            {
                var newregister = new Register(500);

                // TODO create test register

                File.Create("register.json").Close();
                File.WriteAllText("register.json", JsonConvert.SerializeObject(newregister, Formatting.Indented));
                return newregister;
            }

            return (Register)JsonConvert.DeserializeObject(File.ReadAllText("register.json"));
        }
    }
}
