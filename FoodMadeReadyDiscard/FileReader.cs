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
        public FileReader()
        {
            ReadFile();
        }
        public void ReadFile()
        {        
            var FileLines = File.ReadAllLines(@"data\food.txt").ToList();
            Console.WriteLine(FileLines.Count);
            for (int i = 1; i < FileLines.Count; i++)
            {
                string[] split = FileLines[i].Split(',');
                Products.Add(split);
            }

        }
    }
}
