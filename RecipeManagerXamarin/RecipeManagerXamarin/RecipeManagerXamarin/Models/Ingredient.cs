using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeManagerXamarin
{
    public class Ingredient
    {
        public string Name { get; set; } // Name of the ingredient.

        // Constructor for the Ingredient class.
        public Ingredient(string name)
        {
            Name = name;
        }
    
    }
}
