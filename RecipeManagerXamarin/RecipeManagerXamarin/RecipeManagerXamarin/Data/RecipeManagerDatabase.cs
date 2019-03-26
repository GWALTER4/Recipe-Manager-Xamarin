using System;
using System.Collections.Generic;
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
                return _database.Delete<Category>(categoryID);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets all the recipes from the database.
        /// </summary>
        /// <returns>Recipe list</returns>
        public List<Recipe> GetAllRecipes()
        {
            try
            {
                return _database.Table<Recipe>().ToList();
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
    }
}
