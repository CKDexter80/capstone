using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class InventoryMethods
    {
        // Builds inventory list from input csv file
        public Dictionary<string, Snack> ReadInventoryFile(string directory, string fileName)
        {
                        
            string fullPath = Path.Combine(directory, fileName);

            Dictionary<string, Snack> inventoryItems = new Dictionary<string, Snack>();

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {

                    while (!sr.EndOfStream)
                    {

                        string line = sr.ReadLine();


                        string[] items = line.Split('|');

                        if (items[3] == "Chip")
                        {
                            inventoryItems[items[0]] = new Chip(items[1], Decimal.Parse(items[2]));
                        }
                        else if (items[3] == "Drink")
                        {
                            inventoryItems[items[0]] = new Drink(items[1], Decimal.Parse(items[2]));
                        }
                        else if (items[3] == "Candy")
                        {
                            inventoryItems[items[0]] = new Candy(items[1], Decimal.Parse(items[2]));
                        }
                        else if (items[3] == "Gum")
                        {
                            inventoryItems[items[0]] = new Gum(items[1], Decimal.Parse(items[2]));
                        }

                    }
                }
            }
            catch (IOException e)
            {

                Console.WriteLine(e.Message);
            }

            return inventoryItems;

        }

        // Creates blank sales report
        public Dictionary<string, int> CreateBlankReport(Dictionary<string, Snack> inventory)
        {
            Dictionary<string, int> emptyReport = new Dictionary<string, int>();

            foreach (KeyValuePair<string, Snack> item in inventory)
            {
                emptyReport.Add(item.Value.Name, 0);
            }
            return emptyReport;
        }

    }
}
