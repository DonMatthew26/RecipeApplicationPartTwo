using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Recipe App!");

            List<Recipe> recipes = new List<Recipe>();

            while (true)
            {
                Recipe recipe = new Recipe();
                Console.WriteLine("\nEnter recipe details:");

                recipe.EnterRecipeDetails();

                recipes.Add(recipe);

                Console.Write("\nEnter 'continue' to add another recipe, or 'exit' to quit: ");
                string input = Console.ReadLine().ToLower();

                if (input != "continue")
                {
                    break;
                }
            }

            // Sort recipes by name
            recipes = recipes.OrderBy(r => r.Name).ToList();

            Console.WriteLine("\nAll Recipes:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.Write("\nEnter the number of the recipe you want to display: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice >= 1 && choice <= recipes.Count)
            {
                Recipe selectedRecipe = recipes[choice - 1];
                Console.WriteLine($"\nSelected Recipe: {selectedRecipe.Name}");
                Console.WriteLine("Recipe Details:");
                selectedRecipe.DisplayRecipe();
                Console.WriteLine($"Total Calories: {selectedRecipe.CalculateTotalCalories()}");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.");
            }
        }
    }

    class Recipe
    {
        public string Name { get; set; }
        private List<Ingredient> ingredients;
        private List<Ingredient> originalIngredients;
        private List<string> steps;

        public Recipe()
        {
            ingredients = new List<Ingredient>();
            originalIngredients = new List<Ingredient>();
            steps = new List<string>();
        }

        public void EnterRecipeDetails()
        {
            Console.Write("Enter recipe name: ");
            Name = Console.ReadLine();

            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.Write($"Ingredient {i + 1} name: ");
                string name = Console.ReadLine();

                Console.Write($"Quantity for {name}: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write($"Unit of measurement for {name}: ");
                string unit = Console.ReadLine();

                Console.Write($"Number of calories for {name}: ");
                int calories = int.Parse(Console.ReadLine());

                Console.Write($"Food group for {name}: ");
                string foodGroup = Console.ReadLine();

                AddIngredient(name, quantity, unit, calories, foodGroup);
            }

            Console.Write("Enter the number of steps: ");
            int numSteps = int.Parse(Console.ReadLine());

            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"Step {i + 1}: ");
                string description = Console.ReadLine();
                AddStep(description);
            }
        }

        public void DisplayRecipe()
        {
            Console.WriteLine("\nIngredients:");
            double totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
                Console.WriteLine($"  Calories: {ingredient.Calories}");
                Console.WriteLine($"  Food Group: {ingredient.FoodGroup}");
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }

            if (totalCalories > 300)
            {
                Console.WriteLine("\nWarning: Total calories exceed 300!");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }

        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }
            return totalCalories;
        }

        private void AddIngredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Ingredient ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
            ingredients.Add(ingredient);
            originalIngredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup)); // Store original quantity
        }

        private void AddStep(string description)
        {
            steps.Add(description);
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}

