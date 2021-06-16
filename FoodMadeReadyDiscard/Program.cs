using FoodMadeReadyDiscard.Data;
using FoodMadeReadyDiscard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodMadeReadyDiscard
{

    class Program
    {
        static List<Foods> foods;
        static FoodDataContext context = new FoodDataContext();
        static DatabaseOperations databaseOperations = new DatabaseOperations();
        static readonly HttpClient client = new HttpClient();
        static string currentCategory = "";
        static async Task Main(string[] args)
        {
            await ProcessFoodProducts();
            MainMenu();
            
            
     
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
            Console.WriteLine("2 Print a category");
            Console.WriteLine("3 Exit application");
        }
        private static void SelectUserInput(int userInput)
        {
            if (userInput == 1)
                PrintAllAlphabetically();

            if (userInput == 2)
                ChooseCategory();
            


            if (userInput == 3)
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
        static void ChooseCategory()
        {
            Console.WriteLine("Please input the name of the category");
            Console.WriteLine("Starter");
            Console.WriteLine("Meat");
            Console.WriteLine("Toppings");
            Console.WriteLine("Dough");
            Console.WriteLine("Desserts");
            Console.WriteLine("Salads");
            Console.WriteLine("Sauces");
            Console.WriteLine("Return");
            var userInput = Console.ReadLine();
            PrintCategory(userInput);
        }
        static void PrintCategory(string category)
        {
            foreach (var food in foods.Where(d => d.Category == category))
            {
                ReturnShelfLife(food);
            }
            ReturnToMain();
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
        static async Task ProcessFoodProducts()
        {
            var stringTask = client.GetStreamAsync("http://localhost:38953/api/foods");
            foods = await JsonSerializer.DeserializeAsync<List<Foods>>(await stringTask);
        }
    }
}
