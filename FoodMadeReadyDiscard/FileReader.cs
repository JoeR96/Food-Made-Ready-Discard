using FoodMadeReadyDiscard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMadeReadyDiscard
{
    class FileReader
    {
        public static List<string[]> ReadFile()
        {
            List<string[]> Products = new List<string[]>();
            var FileLines = File.ReadAllLines(@"data\Starters").ToList();
 

            foreach (string line in FileLines)
            {
                string[] split = line.Split(',');
                Products.Add(split);
                Console.WriteLine(split[0]);

            }

            return Products;
        }
    }
}
