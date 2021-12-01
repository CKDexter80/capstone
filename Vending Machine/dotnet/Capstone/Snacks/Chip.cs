using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Chip : Snack
    {
        public Chip(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string MessageDisplay()
        {
            return "Crunch Crunch, Yum!";
        }
        public override void InventoryAdjust()
        {
            Quantity -= 1;
        }
    }
}
