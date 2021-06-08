using FoodMadeReadyDiscard.Data;
using FoodMadeReadyDiscard.Models;
using System;
using System.Linq;

namespace FoodMadeReadyDiscard
{

    class Program
    {
        static FoodDataContext context = new FoodDataContext();
        static FileReader fileReader = new FileReader();

        static string userInput;
        static void Main(string[] args)
        {
            FileReader.ReadFile();
            Console.ReadLine();
            //MainMenu();
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

            MainMenu();
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

            MainMenu();
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

            MainMenu();
        }
        static void PrintAllAlphabetically()
        {
            var foods = context.Foods.OrderBy(d => d.Name);
            foreach(var f in foods)
            {
                Console.WriteLine(f.Name + " " + f.DefrostDuration + " " + f.ShelfLifeHours + " " + f.Id);
            }

            MainMenu();
        }
        static void ReturnShelfLife(Foods food)
        {
            Console.WriteLine("Made: " + DateTime.Now);
            Console.WriteLine("Ready: " + DateTime.Now.AddHours(food.DefrostDuration));
            Console.WriteLine("Discard: " + DateTime.Now.AddHours(food.DefrostDuration + food.ShelfLifeHours));
            MainMenu();
        }
        static void MainMenu()
        {

            Console.WriteLine("Please input the option number");
            Console.WriteLine("1 Print all llphabetically");
            Console.WriteLine("2 Add to database");
            Console.WriteLine("3 Remove from database");
            Console.WriteLine("4 Remove Duplicates from the database");
            Console.WriteLine("5 Quit Application");
            userInput = Console.ReadLine();

            if (userInput == "1")
                PrintAllAlphabetically();

            if (userInput == "2")
                AddToDataBase();

            if (userInput == "3")
                RemoveWithIdOrName();

            if (userInput == "4")
                RemoveDuplicatesWithName();

            if (userInput == "5")
                Environment.Exit(0);


            
        }
        private static void RemoveWithIdOrName()
        {
            Console.WriteLine("Please enter the name or the Id of the food you would like to remove");
            string userInput = Console.ReadLine();

            if (userInput.All(char.IsDigit))
                RemoveFromDatabase(Int32.Parse(userInput));

            else
                RemoveFromDatabase(userInput);
        } 
        private static void RemoveDuplicatesWithName()
        {
            Console.WriteLine("Please enter the name or the Id of the foods you would like to remove");
            string userInput = Console.ReadLine();

            RemoveDuplicatesFromDatabase(userInput);
        }
        private static void RemoveDuplicatesFromDatabase(string name)
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

            MainMenu();
        }
    }
}
