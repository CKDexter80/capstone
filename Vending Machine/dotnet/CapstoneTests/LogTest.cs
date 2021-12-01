using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone;
using System.IO;

namespace CapstoneTests
{
    [TestClass]

    public class LogTest
    {
        [TestMethod]
        public void AuditLogTest()
        {
            AuditLog al = new AuditLog();

            al.Log("This is a test text.");
            al.Log("This is the next text.");
            al.Log("This is the best text");

            string dir = Environment.CurrentDirectory;
            string file = "Log.txt";
            string fullPath = Path.Combine(dir, file);

            string expected = "This is a test text.";
            string expected1 = "This is the next text.";
            string expected2 = "This is the best text";

            List<string> actual = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        actual.Add(line);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            CollectionAssert.Contains(actual, expected);
            CollectionAssert.Contains(actual, expected1);
            CollectionAssert.Contains(actual, expected2);
        }
        [TestMethod]
        public void SalesLogTest()
        {
            SalesLog sl = new SalesLog();

            sl.Log("This is a test text.");
            sl.Log("This is the next text.");
            sl.Log("This is the best text");

            string dir = Environment.CurrentDirectory;
            string file = $"Sales_{DateTime.Now:MMddyyyyhhmmsstt}.txt";
            string fullPath = Path.Combine(dir, file);

            List<string> expected = new List<string>();
            expected.Add("This is a test text.");
            expected.Add("This is the next text.");
            expected.Add("This is the best text");

            List<string> actual = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        actual.Add(line);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
