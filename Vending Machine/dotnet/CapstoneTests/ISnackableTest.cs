using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone;

namespace CapstoneTests
{
    [TestClass]
    

    public class ISnackableTest
    {
        [TestMethod]
        public void ChipISnackable()
        {
            Chip chip = new Chip("Pringles", 3.00M);
            
            string expected = "Crunch Crunch, Yum!";
            string actual = chip.MessageDisplay();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DrinkISnackable()
        {
            Drink drink = new Drink("Coke", 2.00M);

            string expected = "Glug Glug, Yum!";
            string actual = drink.MessageDisplay();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GumISnackable()
        {
            Gum gum = new Gum("Big League Chew", 4.00M);

            string expected = "Chew Chew, Yum!";
            string actual = gum.MessageDisplay();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CandyISnackable()
        {
            Candy candy = new Candy("Skittles", 1.00M);

            string expected = "Munch Munch, Yum!";
            string actual = candy.MessageDisplay();

            Assert.AreEqual(expected, actual);
        }
    }
}
