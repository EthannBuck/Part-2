# Recipe Manager

## Introduction

Recipe Manager is a C# console application that allows users to manage recipes by entering details, displaying recipe details, changing scale factors, clearing recipe data, and printing all recipes. The application also includes calorie notifications if the total calories exceed a certain threshold.

## Prerequisites

- .NET Core SDK 3.1 or later
- Visual Studio 2019 or later (optional but recommended for easier development)

## Compilation and Execution Instructions

### Using Visual Studio

1. **Open the Solution:**
   - Open Visual Studio.
   - Go to `File -> Open -> Project/Solution`.
   - Select the `PROGPOE.sln` file.

2. **Build the Solution:**
   - Go to `Build -> Build Solution` or press `Ctrl+Shift+B`.

3. **Run the Application:**
   - Set `PROGPOE` as the startup project (right-click on the project in Solution Explorer and select `Set as Startup Project`).
   - Go to `Debug -> Start Debugging` or press `F5`.

### Using Command Line

1. **Navigate to the Project Directory:**
   ```bash
   cd path/to/PROGPOE

### Changes made to code
1. A new function ConvertTablespoonsToCups was added to the RecipeManager class. This function automatically converts the quantity of ingredients from tablespoons to cups if the quantity is a multiple of 16.
2. The ConvertTablespoonsToCups function is called within the EnterRecipeDetails method when entering ingredient details.
3. Unit tests were added to ensure the functionality of the Recipe class, specifically focusing on total calorie calculations and notifications.

### Link to repository
https://github.com/EthannBuck/Part-2.git 
