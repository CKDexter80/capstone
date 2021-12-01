using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Candy: Snack
    {
        public Candy(string name, decimal price)
        {
            Name = name;
            Price = price;           
        }

        public override string MessageDisplay()
        {
            return "Munch Munch, Yum!";
        }
        public override void InventoryAdjust()
        {                       
                Quantity -= 1;            
        }
    }
}
