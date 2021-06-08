using FoodMadeReadyDiscard.Data;
using FoodMadeReadyDiscard.Models;
using System;
using System.Linq;

namespace FoodMadeReadyDiscard
{
    class Program
    {
        static FoodDataContext context = new FoodDataContext();

        static void Main(string[] args)
        {
            AddTheTrinityOfMeat();
        }

        private static void AddTheTrinityOfMeat()
        {
            Foods Ham = new Foods()
            {
                Name = "Ham",
                DefrostDuration = 1,
                ShelfLifeHours = 4
            };
            context.Foods.Add(Ham);

            Foods Pepperoni = new Foods()
            {
                Name = "Pepperoni",
                DefrostDuration = 2,
                ShelfLifeHours = 1
            };
            context.Foods.Add(Pepperoni);

            Foods Chicken = new Foods()
            {
                Name = "Chicken",
                DefrostDuration = 24,
                ShelfLifeHours = 120
            };
            context.Foods.Add(Chicken);
            context.SaveChanges();
            ReturnShelfLife(Chicken);
        }

        //Need to add validation checks for the integer values
        static void AddToDataBase()
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
        static void RemoveFromDatabase(int foodId)
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
        static void RemoveFromDatabase(string name)
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
        static void PrintAllAlphabetically()
        {
            var foods = context.Foods.OrderBy(d => d.Name);
            foreach(var f in foods)
            {
                Console.WriteLine(f.Name + f.DefrostDuration + f.ShelfLifeHours);
            }
        }
        static void ReturnShelfLife(Foods food)
        {
            Console.WriteLine("Made: " + DateTime.Now);
            Console.WriteLine("Ready: " + DateTime.Now.AddHours(food.DefrostDuration));
            Console.WriteLine("Discard: " + DateTime.Now.AddHours(food.DefrostDuration + food.ShelfLifeHours));
        }
    }
}
