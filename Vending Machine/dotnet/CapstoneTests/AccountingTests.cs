using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
   
    public class AccountingTests
    {
        [TestMethod]
        public void FeedMoneyTest()
        {
            Capstone.Accounting ac = new Capstone.Accounting();

            decimal balance = 4;
            decimal amountFed = 4;

            decimal expected = 8;
            decimal actual = ac.FeedMoney(balance, amountFed);

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void PurchaseItemTest()
        {
            Capstone.Accounting ac = new Capstone.Accounting();

            decimal balance = 8;
            decimal price = 1.50M;

            decimal expected = 6.50M;
            decimal actual = ac.PurchaseItem(balance, price);

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void CalculateTotalSalesTest()
        {
            Capstone.Accounting ac = new Capstone.Accounting();

            decimal gross= 9;
            decimal recent= 1.50M;

            decimal expected = 10.50M;
            decimal actual = ac.CalculateTotalSales(gross, recent);

            Assert.AreEqual(expected, actual);

        }
    }
}
