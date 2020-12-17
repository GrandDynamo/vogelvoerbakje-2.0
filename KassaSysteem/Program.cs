using System;
using System.Reflection;
using System.Linq;

namespace KassaSysteem
{
    class Program
    {
        static Register register;
        static bool quit = false;

        static void Main(string[] args)
        {
            Console.WriteLine($"Register system.");

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

            stock.AddItem(milk);
            stock.AddItem(eggs);
            stock.AddItem(meatballs);
            stock.AddItem(candycanes);
            stock.AddItem(chocolatemilk);
            stock.AddItem(tacoshells);
            stock.AddItem(tacobeef);
            stock.AddItem(tacospices);
            stock.AddItem(creamcheese);

            while (!quit)
            {
                Console.Write("#: ");
                var cmd = Console.ReadLine();

                switch (cmd)
                {
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

                    default:
                        if (cmd.StartsWith("scan "))
                        {
                            if (int.TryParse(cmd.Substring(5), out int id))
                            {
                                // scan
                                Console.WriteLine($"Scanned {id}.");
                                var product = register.GetProduct(id);
                                if (product != null)
                                {
                                    register.AddToReceipt(product);
                                    Console.WriteLine($"Added product {product.GetProductName()} to receipt.");
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
                            if(cmdargs.Length == 2)
                            {
                                double paid = double.Parse(cmdargs[0]);
                                // TODO error handling if invalid paymet method
                                PaymentMethod method = typeof(PaymentMethod).GetEnumValues().Cast<PaymentMethod>().First(x => Enum.GetName(typeof(PaymentMethod), x).ToUpper() == cmdargs[1].ToUpper());

                                Console.WriteLine($"Payment method: {Enum.GetName(typeof(PaymentMethod), method)}");
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
