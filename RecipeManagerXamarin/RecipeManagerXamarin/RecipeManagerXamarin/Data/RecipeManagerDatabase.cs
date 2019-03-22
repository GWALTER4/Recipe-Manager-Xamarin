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

    }
}
