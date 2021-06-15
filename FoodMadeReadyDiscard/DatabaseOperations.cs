using FoodMadeReadyDiscard.Data;
using FoodMadeReadyDiscard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMadeReadyDiscard
{
    class DatabaseOperations
    {
        FoodDataContext context = new FoodDataContext();
        //Need to add 
        public void AddToDataBaseManually()
        {
            Console.WriteLine("Please input the name of the food product");
            string name = Console.ReadLine();
            Console.WriteLine("Please input the defrost duration");
            int defrostDuration = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please input the shelf life duration");
            int shelfLife = Convert.ToInt32(Console.ReadLine());

            Foods newFood = new Foods()
            {
                Name = name,
                DefrostDuration = defrostDuration,
                ShelfLifeHours = shelfLife
            };

            context.Add(newFood);
            context.SaveChanges();
        }
        public void AddFileOfFoodToDatabase()
        {
            FileReader fileReader = new FileReader();
            List<string[]> foods = fileReader.Products;

            for (int i = 0; i < foods.Count; i++)
            {
                var food = CreateFoodFromTextFileLine(foods[i]);
                context.Add(food);
            }

            context.SaveChanges();

        }
        private Foods CreateFoodFromTextFileLine(string[] line)
        {

            Foods newFood = new Foods()
            {
                Name = line[0],
                DefrostDuration = Int32.Parse(line[1]),
                ShelfLifeHours = Int32.Parse(line[2]),
                Category = line[3],
                ProductCode = Int32.Parse(line[4]),
            };

            return newFood;
        }
        public void RemoveFromDatabase(int foodId)
        {
            Foods toRemove = context.Foods.Find(foodId);

            if (toRemove == null)
            {
                Console.WriteLine("Please enter a valid product Id");
                return;
            }


            context.Foods.Remove(toRemove);
            context.SaveChanges();

        }
        public void RemoveFromDatabase(string name)
        {
            Foods toRemove = context.Foods.Where(d => d.Name == name).FirstOrDefault();

            if (toRemove == null)
            {
                Console.WriteLine("Please input the correct product name");
                return;
            }


            context.Foods.Remove(toRemove);
            context.SaveChanges();
        }
        public void RemoveWithIdOrName()
        {
            Console.WriteLine("Please enter the name or the Id of the food you would like to remove");
            string userInput = Console.ReadLine();

            if (userInput.All(char.IsDigit))
                RemoveFromDatabase(Int32.Parse(userInput));

            else
                RemoveFromDatabase(userInput);
        }
        public void RemoveDuplicatesWithName()
        {
            Console.WriteLine("Please enter the name or the Id of the foods you would like to remove");
            string userInput = Console.ReadLine();

            RemoveDuplicatesFromDatabase(userInput);
        }
        private void RemoveDuplicatesFromDatabase(string name)
        {
            Foods[] toRemove = context.Foods.Where(d => d.Name == name).Skip(1).ToArray();


            if (toRemove == null)
            {
                Console.WriteLine("Please enter a valid product Id");
                return;
            }

            for (int i = 0; i < toRemove.Length; i++)
            {
                context.Remove(toRemove[i]);
            }

            context.SaveChanges();
        }
        private void RemoveDuplicatesFromDatabase(int productId)
        {
            Foods[] toRemove = context.Foods.Where(d => d.ProductCode == productId).Skip(1).ToArray();

            for (int i = 1; i < toRemove.Length; i++)
            {
                context.Remove(toRemove[i]);
            }
            context.SaveChanges();
        }
        public void RemoveAllDuplicatesFromDatabase()
        {
            int maxId = context.Foods.Max(p => p.ProductCode);

            for (int i = 0; i < maxId; i++)
            {
                RemoveDuplicatesFromDatabase(i);
            }
        }
    }
}
