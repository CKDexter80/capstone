using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Gum: Snack
    {
        public Gum(string name, decimal price)
        {
            Name = name;
            Price = price;           
        }

        public override string MessageDisplay()
        {
            return "Chew Chew, Yum!";
        }
        public override void InventoryAdjust()
        {
            Quantity -= 1;
        }
    }
}
