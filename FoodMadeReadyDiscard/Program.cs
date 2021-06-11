using FoodMadeReadyDiscard.Data;
using FoodMadeReadyDiscard.Models;
using System;
using System.Linq;

namespace FoodMadeReadyDiscard
{

    class Program
    {
        static FoodDataContext context = new FoodDataContext();
        static DatabaseOperations databaseOperations = new DatabaseOperations();
        static string userInput;
        static void Main(string[] args)
        {     
            MainMenu();
        }
        static void MainMenu()
        {
            Console.WriteLine("Please input the option number");
            Console.WriteLine("1 Print all allphabetically");
            Console.WriteLine("2 Add to database");
            Console.WriteLine("3 Remove from database");
            Console.WriteLine("4 Remove Duplicates from the database");
            Console.WriteLine("6 Add data from starters text file");
            Console.WriteLine("6 Print All Starters");
            Console.WriteLine("7 Exit application");
            userInput = Console.ReadLine();

            if (userInput == "1")
                PrintAllAlphabetically();

            if (userInput == "2")
            {
                databaseOperations.RemoveAllDuplicatesFromDatabase();
            }
            if (userInput == "3")
                databaseOperations.RemoveWithIdOrName();

            if (userInput == "4")
                databaseOperations.RemoveDuplicatesWithName();

            if (userInput == "5")
                AddData();

            if (userInput == "6")
                PrintCategory("Starter");

            if (userInput == "7")
                Environment.Exit(0);


        }
        static void PrintAllAlphabetically()
        {
            var foods = context.Foods.OrderBy(d => d.Name);
            foreach(var f in foods)
            {
                Console.WriteLine("Name: " + f.Name + " Defrost Duration: " + f.DefrostDuration + " Shelf Life: " 
                    + f.ShelfLifeHours + " Product Id: " + f.Id + " Category: " + f.Category);
            }

            MainMenu();
        }
        static void PrintCategory(string category)
        {
            var foods = context.Foods.Where(d => d.Category == category).ToArray();
            Console.WriteLine(foods.Length);
            foreach (var food in foods)
            {
                ReturnShelfLife(food);
            }
          ;
        }
        static void ReturnShelfLife(Foods food)
        {
            Console.WriteLine(food.Name +" Made: " + DateTime.Now + " Ready: " + DateTime.Now.AddHours(food.DefrostDuration)
            + "Discard: " + DateTime.Now.AddHours(food.DefrostDuration + food.ShelfLifeHours));
            MainMenu();
        }
        
        private static void AddData()
        {
            databaseOperations.AddFileOfFoodToDatabase();
        }
    }
}
