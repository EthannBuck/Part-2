// Ethan Buck
// ST10250745
// Group 1

// References
// https://www.geeksforgeeks.org/console-clear-method-in-c-sharp/
// https://raisanenmarkus.github.io/csharp/part3/1/#:~:text=Creating%20a%20new%20list%20is,the%20List%20variable%20is%20List.
// https://wellsb.com/csharp/beginners/create-menu-csharp-console-application

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System;
using System.Collections.Generic;

namespace PROGPOE.Classes
{
    // Delegate definition for calorie notification
    public delegate void CalorieNotificationHandler(string message);

    public class Recipe
    {
        // Properties of the Recipe class
        public string Name { get; set; }
        public List<string> IngredientNames { get; set; } = new List<string>();
        public List<int> IngredientQuantities { get; set; } = new List<int>();
        public List<string> IngredientUnits { get; set; } = new List<string>();
        public List<string> Steps { get; set; } = new List<string>();
        public List<double> IngredientCalories { get; set; } = new List<double>();
        public List<string> IngredientFoodGroups { get; set; } = new List<string>();
        public double ScaleFactor { get; set; } = 1;

        // Event for calorie notification
        public event CalorieNotificationHandler CalorieNotification;

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Calculate total calories in the recipe
        public double TotalCalories
        {
            get
            {
                double total = 0;
                // Sum up the calories of all ingredients
                for (int i = 0; i < IngredientCalories.Count; i++)
                {
                    total += IngredientCalories[i] * (ScaleFactor);
                }
                // Check if total calories exceed 300 and trigger notification
                if (total > 300)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    CalorieNotification?.Invoke($"WARNING: Total calories exceeds 300, this could be too much calories for intake");
                    Console.ResetColor();
                }
                return total;
            }
        }
    }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public class RecipeManager
    {
        private List<Recipe> recipes = new List<Recipe>();
        private int choice;
        private readonly List<string> foodGroups = new List<string>
        {
            "Fruits", "Vegetables", "Grains", "Protein Foods", "Dairy", "Fats and Oils", "Sweets"
        };

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Display the menu options to the user
        public void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("************* MENU **************");
            Console.ResetColor();
            Console.WriteLine("1. Enter Recipe Details");
            Console.WriteLine("2. Display Recipe Details");
            Console.WriteLine("3. Change Scale Factor");
            Console.WriteLine("4. Clear Recipe Data");
            Console.WriteLine("5. Print All Recipes");
            Console.WriteLine("6. Exit");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*********************************");
            Console.ResetColor();
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Run the main program loop
        public void Run()
        {
            do
            {
                DisplayMenu();
                Console.WriteLine("Enter your choice (1-6): ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                // Execute the user's choice
                switch (choice)
                {
                    case 1:
                        EnterRecipeDetails();
                        break;
                    case 2:
                        SearchRecipe();
                        break;
                    case 3:
                        ChangeScaleFactor();
                        break;
                    case 4:
                        ClearRecipeData();
                        break;
                    case 5:
                        PrintAllRecipes();
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6:");
                        break;
                }
            } while (choice != 6);
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Enter the details of a new recipe
        public void EnterRecipeDetails()
        {
            Recipe recipe = new Recipe();

            // Subscribe to the calorie notification event
            recipe.CalorieNotification += NotifyCalorieExceedance;

            Console.WriteLine("Enter recipe name: ");
            recipe.Name = Console.ReadLine();

            Console.WriteLine("Enter the number of ingredients: ");
            try
            {
                int numIngredients = int.Parse(Console.ReadLine());
                for (int i = 0; i < numIngredients; i++)
                {
                    Console.WriteLine("Enter the name of the ingredient: ");
                    recipe.IngredientNames.Add(Console.ReadLine());

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("A calorie is a unit of energy that measures how much energy food provides to the body. The body needs calories to work as it should. Dietary fats are nutrients in food that the body uses to build cell membranes, nerve tissue (like the brain), and hormones. Fat in our diet is a source of calories.");
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.WriteLine("Enter the number of calories in the ingredient: ");
                    double calories;
                    if (!double.TryParse(Console.ReadLine(), out calories) || calories <= 0)
                    {
                        Console.WriteLine("Invalid input for calories. Please enter a positive number.");
                        i--;
                        continue;
                    }
                    recipe.IngredientCalories.Add(calories);

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("A food group is a collection of foods that share similar nutritional properties or biological classifications. Lists of nutrition guides typically divide foods into food groups, and Recommended Dietary Allowance recommends daily servings of each group for a healthy diet.");
                    Console.ResetColor();
                    Console.WriteLine();
                    
                    Console.WriteLine("Select the food group that this ingredient belongs to: ");
                    for (int j = 0; j < foodGroups.Count; j++)
                    {
                        Console.WriteLine($"{j + 1}. {foodGroups[j]}");
                    }

                    int foodGroupChoice;
                    try
                    {
                        foodGroupChoice = int.Parse(Console.ReadLine()) - 1;
                        if (foodGroupChoice < 0 || foodGroupChoice >= foodGroups.Count)
                        {
                            Console.WriteLine("Invalid choice. Please enter a number corresponding to the food group.");
                            i--;
                            continue;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        i--;
                        continue;
                    }

                    recipe.IngredientFoodGroups.Add(foodGroups[foodGroupChoice]);

                    Console.WriteLine("Enter the quantity of the ingredient: ");
                    int quantity;
                    if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid input for quantity. Please enter a positive integer.");
                        i--;
                        continue;
                    }

                    Console.WriteLine("Enter the unit of measurement of the ingredient: ");
                    string unit = Console.ReadLine();

                    // Convert tablespoons to cups if necessary
                    ConvertTablespoonsToCups(ref quantity, ref unit);

                    recipe.IngredientQuantities.Add(quantity);
                    recipe.IngredientUnits.Add(unit);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter valid numbers.");
                return;
            }

            Console.WriteLine("Enter the number of steps: ");
            try
            {
                int numSteps = int.Parse(Console.ReadLine());
                for (int i = 0; i < numSteps; i++)
                {
                    Console.WriteLine("Enter Step " + (i + 1) + ":");
                    recipe.Steps.Add(Console.ReadLine());
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            recipes.Add(recipe);
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Convert tablespoons to cups if the quantity is a multiple of 16
        public void ConvertTablespoonsToCups(ref int quantity, ref string unit)
        {
            if (unit.Equals("tablespoons", StringComparison.OrdinalIgnoreCase) && quantity % 16 == 0)
            {
                quantity /= 16;
                unit = "cups";
            }
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Search for a recipe by name and display its details
        public void SearchRecipe()
        {
            Console.WriteLine("Enter the name of the recipe to display: ");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipes.Find(r => r.Name == recipeName);

            if (recipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }
            DisplayRecipe(recipe);
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Display the details of a recipe
        public void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************************************************");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("RECIPE: " + recipe.Name);
            Console.WriteLine("");
            Console.WriteLine("INGREDIENTS:");
            for (int i = 0; i < recipe.IngredientNames.Count; i++)
            {
                Console.WriteLine($"{recipe.IngredientQuantities[i]} {recipe.IngredientUnits[i]} of {recipe.IngredientNames[i]} (Food Group: {recipe.IngredientFoodGroups[i]}, Calories: {recipe.IngredientCalories[i] * recipe.ScaleFactor})");
            }

            Console.WriteLine("");
            Console.WriteLine("STEPS:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine($"Step {i + 1}: {recipe.Steps[i]}");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Between 200 and 400 calories for breakfast, 500-700 calories for lunch, and 500-700 calories for dinner.");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine($"TOTAL CALORIES: {recipe.TotalCalories}");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************************************************");
            Console.ResetColor();
            Console.WriteLine();
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Change the scale factor of a recipe
        public void ChangeScaleFactor()
        {
            Console.WriteLine("Enter the name of the recipe to scale: ");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipes.Find(r => r.Name == recipeName);

            if (recipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine("What would you like to change the scale factor to? '0.5', '1', '2', or '3' : ");
            try
            {
                double scaleFactor = double.Parse(Console.ReadLine());
                if (scaleFactor <= 0)
                {
                    Console.WriteLine("Invalid input. Scale factor must be a positive number.");
                    return;
                }
                recipe.ScaleFactor = scaleFactor;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            // Update the quantities based on the new scale factor
            for (int i = 0; i < recipe.IngredientQuantities.Count; i++)
            {
                recipe.IngredientQuantities[i] = (int)(recipe.IngredientQuantities[i] * recipe.ScaleFactor);
            }

            Console.WriteLine("Recipe scaled successfully.");
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Clear the data of a specific recipe
        public void ClearRecipeData()
        {
            Console.WriteLine("Enter the name of the recipe to clear: ");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipes.Find(r => r.Name == recipeName);

            if (recipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            recipes.Remove(recipe);
            Console.WriteLine("Recipe data has been cleared.");
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Print all recipes in the list
        public void PrintAllRecipes()        
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************************************************");
            Console.ResetColor();
            Console.WriteLine();

            // Sort and display the recipe names
            recipes.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
            Console.WriteLine("Recipe Names: ");
            foreach (var recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************************************************");
            Console.ResetColor();
            Console.WriteLine();
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // Notify the user if the total calories exceed 300
        public void NotifyCalorieExceedance(string message)
        {
            Console.WriteLine(message);
        }
    }
}

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*******************************END OF FILE***********************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
