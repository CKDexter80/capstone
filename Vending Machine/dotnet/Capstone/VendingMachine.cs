using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class VendingMachine
    {
        public VendingMachine(Dictionary<string, Snack> inventory, Dictionary<string, int> quantitySold)
        {
            Inventory = inventory;
            QuantitySold = quantitySold;

        }

        public Dictionary<string, Snack> Inventory { get; private set; }
        public decimal Balance { get; set; } = 0;
        public decimal GrossSales { get; set; }
        public Dictionary<string, int> QuantitySold { get; set; }


        public void MainMenu()
        {
            Console.Clear();
            Output.Greeting();
            Output.DisplayMainMenu();
            string menuInput = Input.GetMenuInput();

            if (menuInput == "1")
            {
                Console.Clear();
                Output.DisplayInventory(Inventory);
                Console.WriteLine();
                Console.WriteLine("Press enter to go back to the Main Menu");
                Console.ReadLine();
            }
            else if (menuInput == "2")
            {
                PurchaseMenu();
            }
            else if (menuInput == "3")
            {
                Environment.Exit(0);
            }
            else if (menuInput == "4")
            {
                foreach (KeyValuePair<string, int> snackItem in QuantitySold)
                {
                    LogHelper.Log(LogTypes.Sales, $"{snackItem.Key}|{snackItem.Value}");
                }
                LogHelper.Log(LogTypes.Sales, $"TOTAL SALES ${GrossSales}");

                Console.Clear();
                Console.WriteLine("Generating Sales Report.....");
                Console.WriteLine("Press enter to go back to the Main Menu");
                Console.ReadLine();
            }

            MainMenu();
        }

        public void PurchaseMenu()
        {
            Accounting ac = new Accounting();
            Console.Clear();
            Output.DisplayPurchaseMenu();
            Output.DisplayCurrentMoney(Balance);
            string menuInput = Input.GetMenuInput();
            

            if (menuInput == "1")
            {
                Console.Clear();
                Output.HowMuchMoney();
                decimal amountFed = Input.GetMoney();                
                Balance = ac.FeedMoney(Balance, amountFed);
                LogHelper.Log(LogTypes.Audit, $"{DateTime.Now} FEED MONEY: ${amountFed.ToString("0.00")} ${Balance.ToString("0.00")}");
                Console.WriteLine($"Success! You've added ${amountFed.ToString("0.00")}");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else if (menuInput == "2")
            {
                if (Balance <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Please add money before making a purchase");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Output.DisplayInventory(Inventory);
                    Console.WriteLine();
                    Output.DisplayCurrentMoney(Balance);
                    Console.WriteLine();
                    Console.WriteLine("Please select an item from above:");
                    string itemSelected = Input.GetMenuInput().ToUpper();
                    SelectSnack(itemSelected);
                }
            }
            else if (menuInput == "3")
            {
                Console.Clear();
                ac.MakeChange(Balance);
                LogHelper.Log(LogTypes.Audit, $"{DateTime.Now} GIVE CHANGE: ${Balance} $0.00");
                Balance = 0;
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                MainMenu();
            }

            PurchaseMenu();
        }

        // If the snack selection is valid, balance and inventory are adjusted and the transaction is logged in the Audit Log.
        public void SelectSnack(string itemSelected)
        {

            Accounting ac = new Accounting();

            if (Inventory.ContainsKey(itemSelected) && Inventory[itemSelected].Price <= Balance)
            {
                Balance = ac.PurchaseItem(Balance, Inventory[itemSelected].Price);
                Inventory[itemSelected].InventoryAdjust();
                GrossSales = ac.CalculateTotalSales(GrossSales, Inventory[itemSelected].Price);
                QuantitySold[Inventory[itemSelected].Name] += 1;
                LogHelper.Log(LogTypes.Audit, $"{DateTime.Now} {Inventory[itemSelected].Name} {itemSelected} ${Balance + Inventory[itemSelected].Price} ${Balance}");
                Console.Clear();
                Console.WriteLine($"{Inventory[itemSelected].MessageDisplay()}");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                // TODO: Ask user to make another purchase. If yes, send back to 
            }
            else if (!Inventory.ContainsKey(itemSelected))
            {
                Console.Clear();
                Console.WriteLine("Item does not exist!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else if (Inventory[itemSelected].Quantity == 0)
            {
                Console.Clear();
                Console.WriteLine("Item is sold out! Please select a different item");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();

            }
            else if (Inventory[itemSelected].Price > Balance)
            {
                Console.Clear();
                Console.WriteLine("Current balance is too low to make this purchase, please add more money");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

    }
}
