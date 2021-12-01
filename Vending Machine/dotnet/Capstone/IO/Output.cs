using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Output
    {
        public static void Greeting()
        {
            Console.WriteLine("Welcome to the Vendo-Matic 800 Vending Machine");
            Console.WriteLine();
        }

        public static void HowMuchMoney()
        {
            Console.WriteLine("How much would you like to feed (Whole Dollar Amounts Only):");
        }

        public static void DisplayMainMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine();
            Console.WriteLine("Please select from the following items:");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
        }

        public static void DisplayPurchaseMenu()
        {
            Console.WriteLine("Purchase Menu");
            Console.WriteLine();
            Console.WriteLine("Please select from the following items:");
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine();
        }

        public static void DisplayCurrentMoney(decimal balance)
        {
            Console.WriteLine($"Current Balance: ${balance.ToString("0.00")}");
        }

        public static void DisplayInventory(Dictionary<string, Snack> inventory)
        {
            Console.WriteLine("Current Inventory:");
            Console.WriteLine();

            foreach (KeyValuePair<string, Snack> item in inventory)
            {
                if (item.Value.Quantity == 0)
                {
                    Console.WriteLine($"{item.Key} {item.Value.Name} ${item.Value.Price} QTY: SOLD OUT");
                }
                else
                {
                    Console.WriteLine($"{item.Key} {item.Value.Name} ${item.Value.Price} QTY: {item.Value.Quantity}");
                }               
            }
        }
    }
}
