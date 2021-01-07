using System;
using System.Reflection;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace KassaSysteem
{
    class Program
    {
        static Register register;
        static bool quit = false;
        static String password = "1234";

        static void Main(string[] args)
        {
            // Making sure color is correct
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Status:");
            Console.ForegroundColor = ConsoleColor.Green;
            Thread.Sleep(400);
            Console.Write(" online");
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"         Register system.         ");
            Console.WriteLine($"Type 'help' for a command listing.\n" +
                              $"----------------------------------");
            Console.ForegroundColor = ConsoleColor.White;

            var stock = new Stock();
            // Entry into program.
            register = new Register(500, stock);

            // TODO create test register
            var milk = new Product(0, "LekkerMelk", 5.0, 0.0);
            var eggs = new Product(1, "Eieren in doos", 3.0, 5.0);
            var meatballs = new Product(2, "Gehaktbal voorgebakken", 6.0, 1.0);
            var candycanes = new Product(3, "Kerst candy canes", 2.0, 0.0);
            var chocolatemilk = new Product(4, "Chocolademelk", 5.0, 0.0);
            var tacoshells = new Product(5, "Tacoschelpen", 3.0, 0.0);
            var tacobeef = new Product(6, "Taco vlees", 6.0, 1.0);
            var tacospices = new Product(7, "Taco kruiden", 1.0, 0.0);
            var creamcheese = new Product(8, "Roomkaas", 5.0, 0.0);

            register.AddAllowedCard(PaymentMethod.Cash);
            register.AddAllowedCard(PaymentMethod.MasterCard);
            register.AddAllowedCard(PaymentMethod.Visa);
            //register.AddAllowedCard(PaymentMethod.DebitCard);

            stock.AddItem(milk);
            stock.AddItem(eggs);
            stock.AddItem(meatballs);
            stock.AddItem(candycanes);
            stock.AddItem(chocolatemilk);
            stock.AddItem(tacoshells);
            stock.AddItem(tacobeef);
            stock.AddItem(tacospices);
            stock.AddItem(creamcheese);

            // lekker lekker tacopakket
            var sale = new Sale(5);
            sale.AddProductToSale(tacoshells);
            sale.AddProductToSale(tacobeef);
            sale.AddProductToSale(tacospices);
            sale.AddProductToSale(creamcheese);

            register.AddSale(sale);

            while (!quit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("> ");
                Console.ForegroundColor = ConsoleColor.Green;
                var cmd = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                switch (cmd)
                {
                    case "help":
                    case "?":
                    case "h":
                        Console.WriteLine(
                            $"Command help:" +
                            $"\n    help: shows command help. (h, ?)" +
                            $"\n    quit: quits the program. (q, exit, stop)" +
                            $"\n    subtotal: gets current subtotal." +
                            $"\n    sales: lists available sales." +
                            $"\n    scan [id]: scans a product by id." +
                            $"\n    price [id]: gets the price for a product." +
                            $"\n    pay [amount] cash: pays, prints receipts and starts a new receipt." +
                            $"\n    pay [card]: pays, prints receipts and starts a new receipt." +
                            $"\n    listsold: prints the sold items." +
                            $"\n    acceptedpayments: prints the payment typed that can be used to pay." +
                            $"\n    " +
                            $"\nRestricted acces:" +
                            $"\n    compatiblepayments: prints the payment typed that are compatible with the system." +
                            $"\n    addpaymentmethod [method]: adds a compatible payment methods to the acceptedpayments." +
                            $"\n    removepaymentmethod [method]: removes a compatible payment methods from the acceptedpayments."
                            );
                        break;
                    case "listsold":
                        register.PrintAllSoldProductFromReceipts();
                        break;

                    case "q":
                    case "quit":
                    case "exit":
                    case "stop":
                        quit = true;
                        Console.WriteLine("Shutting down...");
                        break;

                    case "subtotal":
                        Console.WriteLine($"Current subtotal: {register.GetSubTotal().ToString("c", new NumberFormatInfo() { CurrencySymbol = "EUR " })}");
                        break;

                    case "sales":
                        var sales = register.GetSales();

                        // super coole hackerman one liner om die sales bij elkaar te hacken met prijs en item ids in ééN LIJN
                        var salesString = sales.Select(x => $"{string.Join(", ", x.GetAllItemFromList().Select(y => $"{y.GetProductId()}({y.GetProductName()})"))}: {x.GetPriceSale()}");

                        Console.WriteLine($"Sales:\n\n{string.Join(",\n", salesString)}");
                        break;

                    default:
                        if (cmd.StartsWith("scan "))
                        {
                            if (int.TryParse(cmd.Substring(5), out int id))
                            {
                                // scan
                                Console.WriteLine($"Scanning item {id}.");
                                var product = register.GetProduct(id);
                                if (product != null)
                                {
                                    register.AddToReceipt(product);
                                    Console.WriteLine($"Added product {product.GetProductName()} to receipt.");
                                }
                                else
                                {
                                    Console.WriteLine($"Error: Product with id {id} not found");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: Invalid id");
                            }
                        }
                        else if (cmd.StartsWith("price "))
                        {
                            if (int.TryParse(cmd.Substring(5), out int id))
                            {
                                // scan
                                Console.WriteLine($"Scanned {id}.");
                                var product = register.GetProduct(id);
                                if (product != null)
                                {
                                    Console.WriteLine($"Price for {product.GetProductName()}: {product.GetPrice().ToString("c", new NumberFormatInfo() { CurrencySymbol = "EUR " })}.");
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid product {id}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("invalid id");
                            }
                        }
                        else if (cmd.StartsWith("pay "))
                        {
                            var cmdargs = cmd.Substring(4).Split(' ');
                            if (cmdargs.Length == 2)
                            {
                                if (cmdargs[1] != nameof(PaymentMethod.Cash).ToLower())
                                {
                                    Console.WriteLine("Invalid payment method");
                                    break;
                                }
                                double paid = double.Parse(cmdargs[0]);
                                // TODO error handling if invalid paymet method
                                var methods = typeof(PaymentMethod).GetEnumValues().Cast<PaymentMethod>();

                                if (!methods.Any(x => Enum.GetName(typeof(PaymentMethod), x).ToUpper() == cmdargs[1].ToUpper()))
                                {
                                    Console.WriteLine("Invalid payment method");
                                    break;
                                }

                                PaymentMethod method = methods.First(x => Enum.GetName(typeof(PaymentMethod), x).ToUpper() == cmdargs[1].ToUpper());

                                if (register.Pay(method, paid, out double change))
                                {
                                    Console.WriteLine($"Paid {paid.ToString("c", new NumberFormatInfo() { CurrencySymbol = "EUR " })} with {Enum.GetName(typeof(PaymentMethod), method)}.");
                                }
                                else
                                {
                                    Console.WriteLine($"Not enough cash! Sub total is {register.GetSubTotal()}!");
                                }

                                Console.WriteLine($"Paid back {change.ToString("c", new NumberFormatInfo() { CurrencySymbol = "EUR " })} to the customer.");
                            }

                            else if (cmdargs.Length == 1)
                            {
                                if (register.GetPaymentMethods().Any(e => e.ToString().ToLower().Equals(cmdargs[0].ToLower())))
                                {
                                    if (cmdargs[0] == nameof(PaymentMethod.Cash).ToLower())
                                    {
                                        Console.WriteLine("Specify the amount of cash.");
                                        break;
                                    }
                                    double amount = register.GetSubTotal();
                                    PaymentMethod method = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), cmdargs[0], true);
                                    register.Pay(method, register.GetSubTotal());
                                    Console.WriteLine($"Paid {amount.ToString("c", new NumberFormatInfo() { CurrencySymbol = "EUR " })} with {Enum.GetName(typeof(PaymentMethod), method)}.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid payment option.");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unknown action.");
                                break;
                            }
                        }
                        else if (cmd.StartsWith("receipt"))
                        {
                            register.PrintReceipt();
                        }
                        else if (cmd.StartsWith("acceptedpayments"))
                        {
                            register.PrintPaymentMethods();
                        }
                        else if (cmd.StartsWith("compatiblepayments"))
                        {
                            Console.WriteLine("Insert password:\n");
                            String input = "";
                            while (input != password)
                            {
                                input = Console.ReadLine();
                            }
                            register.PrintPaymentCompatible();
                        }
                        else if (cmd.StartsWith("addpaymentmethod"))
                        {
                            Console.WriteLine("Insert password:\n");
                            String input = "";
                            while (input != password)
                            {
                                input = Console.ReadLine();
                            }

                            var cmdargs = cmd.Substring(4).Split(' ');
                            if (cmdargs.Length == 2)
                            {
                                Console.WriteLine(cmdargs[1]);

                                try
                                {
                                    register.AddAllowedCard((PaymentMethod)Enum.Parse(typeof(PaymentMethod), cmdargs[1]));
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Invalid payment method");
                                    break;
                                }                  
                            }
                        }
                        else if (cmd.StartsWith("removepaymentmethod"))
                        {
                            Console.WriteLine("Insert password:\n");
                            String input = "";
                            while (input != password)
                            {
                                input = Console.ReadLine();
                            }

                            var cmdargs = cmd.Substring(4).Split(' ');
                            if (cmdargs.Length == 2)
                            {
                                if (cmdargs[1] != nameof(PaymentMethod).ToLower())
                                {
                                    Console.WriteLine("Invalid payment method");
                                    break;
                                }
                                register.RemoveAllowedCard((PaymentMethod)Enum.Parse(typeof(PaymentMethod), cmdargs[1]));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unknown action.");
                        }
                        break;
                }
            }
        }
    }
}
