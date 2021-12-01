using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Drink : Snack
    {
        public Drink (string name, decimal price)
        {
            Name = name;
            Price = price;            
        }

        public override string MessageDisplay()
        {
            return "Glug Glug, Yum!";
        }
        public override void InventoryAdjust()
        {
            Quantity -= 1;
        }
    }
}