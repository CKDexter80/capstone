using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Accounting
    {
        public decimal FeedMoney(decimal balance, decimal amountFed)
        {
            try
            {
                decimal newBalance = balance + amountFed;
                return newBalance;
            }
            catch (Exception)
            {                
                return balance;
            }             
        }

        public decimal PurchaseItem(decimal balance, decimal itemPrice)
        {
            decimal newBalance = balance - itemPrice;
            return newBalance;
        }

        public decimal CalculateTotalSales(decimal grossSales, decimal recentSale)
        {
            grossSales += recentSale;
            return grossSales;
        }

        // Outputs a string with the users "change" and sets balance to zero
        public decimal MakeChange(decimal balance)
        {
            decimal balanceInCents = balance * 100;
            decimal quarterCount = 0;
            decimal dimeCount = 0;
            decimal nickelCount = 0;


            if (balanceInCents == 0)
            {
                Console.WriteLine($"Your balance is 0, no change for you!");
            }
            else if (balanceInCents % 25 == 0)
            {
                quarterCount = Math.Floor(balanceInCents / 25);
                Console.WriteLine($"Your change is {balance}, here are {quarterCount} quarters");
            }
            else
            {
                quarterCount = Math.Floor(balanceInCents / 25);
                balanceInCents -= quarterCount * 25;

                if (balanceInCents % 10 == 0)
                {
                    dimeCount = Math.Floor(balanceInCents / 10);
                    Console.WriteLine($"Your change is {balance}, here are {quarterCount} quarters and {dimeCount} dimes");
                }
                else
                {
                    dimeCount = Math.Floor(balanceInCents / 10);
                    balanceInCents -= dimeCount * 10;
                    nickelCount = Math.Floor(balanceInCents / 5);
                    Console.WriteLine($"Your change is {balance}, here are {quarterCount} quarters and {dimeCount} dimes and {nickelCount} nickels");
                }

            }

            return 0;
        }
    }
}
