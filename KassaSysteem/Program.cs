using System;
using System.Reflection;

namespace KassaSysteem
{
    class Program
    {
        static Register register;

        static void Main(string[] args)
        {
            Console.WriteLine($"Register system.");

            // Entry into program.
            register = Register.DeserializeFromDisk();
        }
    }
}
