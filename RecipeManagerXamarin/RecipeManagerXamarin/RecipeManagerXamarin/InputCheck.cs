using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RecipeManagerXamarin
{
    class InputCheck
    {
        #region PROPERTIES
        // Declares an instance of the AddAppointment class.
        private static InputCheck _instance;

        // Instantiates a read-only object to use as a lock for the singleton.
        private static readonly object _lock = new object();

        // Ensures only one instance of the class can be instantiated.
        public static InputCheck GetInputCheckInstance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new InputCheck();
                    }

                    return _instance;
                }
            }
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Checks if a category name contains valid characters.
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <returns>bool</returns>
        public bool IsCategoryNameValid(string categoryName)
        {
            // Uses a regex that only allows alphabetical characters and spaces to
            // validate the user's inputted category name.
            return Regex.IsMatch(categoryName, "[a-zA-Z\\s]+");
        }

        /// <summary>
        /// Checks if an ingredinet name contains valid characters.
        /// </summary>
        /// <param name="categoryName">Ingredient name</param>
        /// <returns>bool</returns>
        public bool IsIngredientNameValid(string ingredientName)
        {
            return Regex.IsMatch(ingredientName, "[a-zA-Z\\s\\d]+");
        }

        /// <summary>
        /// Checks if an instruction's text contains valid characters.
        /// </summary>
        /// <param name="categoryName">Instruction text</param>
        /// <returns>bool</returns>
        public bool IsInstructionTextValid(string instructionText)
        {
            return Regex.IsMatch(instructionText, "[a-zA-Z\\s\\d()\\-.,]+");
        }
        #endregion
    }
}
