using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone;

namespace CapstoneTests
{
    [TestClass]

    public class ExtractInventoryTest
    {
        [DataTestMethod]
        [DataRow("A1")]
        [DataRow("A2")]
        [DataRow("A3")]
        [DataRow("A4")]
        public void ExtractInventory(string key)
        {
            InventoryMethods im = new InventoryMethods();

            string directory = Environment.CurrentDirectory;
            string fileName = "test-1.txt";

            Dictionary<string, Snack> expected = new Dictionary<string, Snack>();
            expected["A1"] = new Chip("Potato Crisps", 3.05M);
            expected["A2"] = new Chip("Stackers", 1.45M);
            expected["A3"] = new Chip("Grain Waves", 2.75M);
            expected["A4"] = new Chip("Cloud Popcorn", 3.65M);

            Dictionary<string, Snack> actual = im.ReadInventoryFile(directory, fileName);

            Snack expectedObj = expected[key];
            Snack actualObj = actual[key];


            Assert.AreEqual(expectedObj.Name, actualObj.Name);
            Assert.AreEqual(expectedObj.Price, actualObj.Price);
            Assert.AreEqual(expectedObj.Quantity, actualObj.Quantity);
        }
    }
}










