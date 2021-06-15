using FoodMadeReadyDiscard.Data;
using FoodMadeReadyDiscard.Models;
using System;
using System.Linq;
using System.Net.Http;

namespace FoodMadeReadyDiscard
{

    class Program
    {
        static FoodDataContext context = new FoodDataContext();
        static DatabaseOperations databaseOperations = new DatabaseOperations();
        static string userInput;
        static void Main(string[] args)
        {
            Console.WriteLine("hi");
            MainMenu();
            HttpClient client = new HttpClient();
            var foodsResponseTask = client.GetAsync("http://localhost:38953/api/foods");
            foodsResponseTask.Wait();
            Console.WriteLine("hi");
            if (foodsResponseTask.IsCompleted)
            {
                var result = foodsResponseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var foods = result.Content.ReadAsStringAsync();
                    foods.Wait();
                    Console.WriteLine(foods.Result);
                    Console.WriteLine(foods.Result.Length);
                    Console.ReadLine();
                }
                
            }
            Console.WriteLine(foodsResponseTask.Result);
            Console.WriteLine("hi");
        }
        static void MainMenu()
        {
            bool notValid = true;
            int userInput = 0;

            while (notValid == true)
            {
                try
                {
                    PrintUserInputOptions();
                    userInput = Convert.ToInt32(Console.ReadLine());

                    while (!Toolkit.BetweenRanges(1, 7, userInput))
                    {
                        Console.WriteLine("Incorrect, please input a number between 1 and 7");
                        userInput = Convert.ToInt32(Console.ReadLine());
                    }

                    notValid = false;
                }
                catch
                {
                    Console.WriteLine("Incorrect, please input a number between 1 and 7");
                }
            }

            SelectUserInput(userInput);

        }
        private static void PrintUserInputOptions()
        {
            Console.WriteLine("Please input the option number");
            Console.WriteLine("1 Print all allphabetically");
            Console.WriteLine("2 Add to database");
            Console.WriteLine("3 Remove from database");
            Console.WriteLine("4 Remove Duplicates from the database");
            Console.WriteLine("6 Add data from starters text file");
            Console.WriteLine("6 Print All Starters");
            Console.WriteLine("7 Exit application");
        }
        private static void SelectUserInput(int userInput)
        {
            if (userInput == 1)
                PrintAllAlphabetically();

            if (userInput == 2)
            {
                databaseOperations.RemoveAllDuplicatesFromDatabase();
            }
            if (userInput == 3)
                databaseOperations.RemoveWithIdOrName();

            if (userInput == 4)
                databaseOperations.RemoveDuplicatesWithName();

            if (userInput == 5)
                AddData();

            if (userInput == 6)
                PrintCategory("Starter");

            if (userInput == 7)
                Environment.Exit(0);
        }  
        private static void ReturnToMain()
        {
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadLine();
            MainMenu();
        }
        static void PrintAllAlphabetically()
        {
            var foods = context.Foods.OrderBy(d => d.Name);
            foreach(var f in foods)
            {
                Console.WriteLine("Name: " + f.Name + " Defrost Duration: " + f.DefrostDuration + " Shelf Life: " 
                    + f.ShelfLifeHours + " Product Id: " + f.Id + " Category: " + f.Category);
            }
           
        }
        static void PrintCategory(string category)
        {
            var foods = context.Foods.Where(d => d.Category == category).ToArray();
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
        }      
        private static void AddData()
        {
            databaseOperations.AddFileOfFoodToDatabase();
        }
    }
}
