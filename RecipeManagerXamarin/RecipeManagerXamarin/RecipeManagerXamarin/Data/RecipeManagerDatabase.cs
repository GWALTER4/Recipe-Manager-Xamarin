using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace RecipeManagerXamarin
{
    public class RecipeManagerDatabase
    {

        // Stores a connection to the database.
        private SQLiteConnection _database;

        /// <summary>
        /// Constructor for the RecipeManagerDatabase class.
        /// </summary>
        /// <param name="dbPath">Path to the database.</param>
        public RecipeManagerDatabase(string dbPath)
        {
            // Creates a new connection to the database.
            _database = new SQLiteConnection(dbPath);

            // Creates the database tables.
            _database.CreateTable<Category>();
            _database.CreateTable<Recipe>();
            _database.CreateTable<Instruction>();
        }

        /// <summary>
        /// Inserts a category into the database.
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns>Code value</returns>
        public int InsertCategory(string name)
        {
            try
            {
                return _database.Insert(new Category { Name = name });
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets all the categories from the database.
        /// </summary>
        /// <returns>Category list</returns>
        public List<Category> GetAllCategories()
        {
            try
            {
                return _database.Table<Category>().ToList();
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }

        /// <summary>
        /// Deletes a category from the database.
        /// </summary>
        /// <param name="categoryID">Category ID</param>
        /// <returns>Row count</returns>
        public int DeleteCategory(int categoryID)
        {
            try
            {
                // Deletes all the instructions attached to all the recipes.
                var recipeList = _database.Query<Recipe>("SELECT * FROM recipe");
                foreach(var recipe in recipeList)
                {
                    DeleteRecipe(recipe);
                }

                // Deletes all the recipes attached to the category.
                _database.Query<Recipe>("DELETE FROM recipe WHERE CategoryID = ?", categoryID);
                return _database.Delete<Category>(categoryID);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets all the recipes for a particular category from the database.
        /// </summary>
        /// <returns>Recipe list</returns>
        public List<Recipe> GetRecipes(Category category)
        {
            try
            {
                // Creates a list of instructions.
                List<Recipe> recipeList = new List<Recipe>();

                // Gets all the recipes for a category from the database.
                var recipes = _database.Query<Recipe>("SELECT * FROM recipe WHERE CategoryID = ?", category.ID);

                // Iterates through the recipes and adds them to the list.
                foreach (var recipe in recipes)
                {
                    recipeList.Add(recipe as Recipe);
                }

                return recipeList;
            }
            catch (Exception ex)
            {
                return new List<Recipe>();
            }
        }

        /// <summary>
        /// Inserts a recipe into the database.
        /// </summary>
        /// <param name="recipe">Recipe</param>
        /// <param name="instructionList">Instruction list</param>
        /// <returns>Code value</returns>
        public int InsertRecipe(Recipe recipe, Instruction[] instructionList)
        {
            try
            {
                _database.Insert(recipe);
            }
            catch (Exception ex)
            {
                return 0;
            }

            try
            {
                // Iterates through the instructions.
                for(int i = 0; i < instructionList.Length; i++)
                {
                    // Inserts the instructions into the database.
                    int value = InsertInstruction(recipe.ID, instructionList[i], i + 1);

                    // Throws an exception if the instruction was not inserted.
                    if(value < 1)
                    {
                        throw new Exception();
                    }                
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// Inserts an instruction into the database.
        /// </summary>
        /// <param name="recipeID">Recipe ID</param>
        /// <param name="instruction">Instruction</param>
        /// <param name="sequenceNumber">Sequence number</param>
        /// <returns></returns>
        private int InsertInstruction(int recipeID, Instruction instruction, int sequenceNumber)
        {
            try
            {
                instruction.RecipeID = recipeID;
                instruction.SequenceNumber = sequenceNumber;
                return _database.Insert(instruction);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Deletes a recipe from the database.
        /// </summary>
        /// <param name="recipeID">Recipe ID</param>
        /// <returns>Row count</returns>
        public int DeleteRecipe(Recipe recipe)
        {
            try
            {
                // Deletes all the instructions attached to the recipe.
                _database.Query<Instruction>("DELETE FROM instruction WHERE RecipeID = ?", recipe.ID);

                // Deletes the recipe.
                return _database.Delete(recipe);             
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Updates a recipe's image file path.
        /// </summary>
        /// <param name="recipe">Recipe object</param>
        /// <param name="filePath">File path</param>
        /// <returns>Code value</returns>
        public int UpdateRecipeImagePath(Recipe recipe, string filePath)
        {
            try
            {
                // Checks if the file path is null.
                if(recipe.ImagePath != null)
                {
                    // Deletes the old image.
                    File.Delete(recipe.ImagePath);
                }

                // Updates the recipe with the new image file path.
                string queryString = "UPDATE recipe SET ImagePath = '" + filePath + "' WHERE ID = " + recipe.ID;
                _database.Query<Recipe>(queryString);

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets all the instructions for a recipe.
        /// </summary>
        /// <param name="recipe">Recipe object</param>
        /// <returns>Instruction list</returns>
        public List<Instruction> GetInstructions(Recipe recipe)
        {
            // Creates a list of instructions.
            List<Instruction> instructionList = new List<Instruction>();

            try
            {
                // Gets all the instructions for a recipe from the database.
                var instructions = _database.Query<Instruction>("SELECT * FROM instruction WHERE RecipeID = ?", recipe.ID);

                // Iterates through the instructions and adds them to the list.
                foreach (var instruction in instructions)
                {
                    instructionList.Add(instruction as Instruction);
                }

                return instructionList;
            }
            catch (Exception ex)
            {
                return instructionList;
            }      
        }
    }
}
