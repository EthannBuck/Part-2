// Ethan Buck
// ST10250745
// Group 1

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*****************************************************************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PROGPOE.Classes;

namespace RecipeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TotalCalories_WhenCalled_ReturnsCorrectTotal()
        {
            // Arrange
            var recipe = new Recipe
            {
                IngredientCalories = new List<double> { 100, 200, 50 },
                ScaleFactor = 1
            };

            // Act
            var totalCalories = recipe.TotalCalories;

            // Assert
            Assert.AreEqual(350, totalCalories);
        }

        [TestMethod]
        public void TotalCalories_WithScaling_ReturnsCorrectTotal()
        {
            // Arrange
            var recipe = new Recipe
            {
                IngredientCalories = new List<double> { 100, 200 },
                ScaleFactor = 2
            };

            // Act
            var totalCalories = recipe.TotalCalories;

            // Assert
            Assert.AreEqual(600, totalCalories);
        }

        [TestMethod]
        public void TotalCalories_Exceeds300_TriggersCalorieNotification()
        {
            // Arrange
            var recipe = new Recipe
            {
                IngredientCalories = new List<double> { 150, 200 },
                ScaleFactor = 1
            };
            string receivedMessage = null;
            recipe.CalorieNotification += message => receivedMessage = message;

            // Act
            var totalCalories = recipe.TotalCalories;

            // Assert
            Assert.AreEqual(350, totalCalories);
            Assert.AreEqual("WARNING: Total calories exceeds 300", receivedMessage);
        }

        [TestMethod]
        public void TotalCalories_DoesNotExceed300_DoesNotTriggerCalorieNotification()
        {
            // Arrange
            var recipe = new Recipe
            {
                IngredientCalories = new List<double> { 100, 50 },
                ScaleFactor = 1
            };
            string receivedMessage = null;
            recipe.CalorieNotification += message => receivedMessage = message;

            // Act
            var totalCalories = recipe.TotalCalories;

            // Assert
            Assert.AreEqual(150, totalCalories);
            Assert.IsNull(receivedMessage);
        }
    }
}

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*******************************END OF FILE***********************>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
