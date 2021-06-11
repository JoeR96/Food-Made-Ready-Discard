using FoodMadeReadyDiscard.Data;
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

        public List<String[]> Products = new List<String[]>();
        public FileReader(string category)
        {
            ReadFile(category);
        }

        public void ReadFile(string category)
        {
            
            var FileLines = File.ReadAllLines(@"data\"+category).ToList();

            for (int i = 1; i < FileLines.Count; i++)
            {
                string[] split = FileLines[i].Split(',');
                Products.Add(split);
            }

        }
    }
}
