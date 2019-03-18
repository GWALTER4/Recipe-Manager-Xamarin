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

        public string StatusMessage { get; set; }

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

        public int AddNewCategory(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception("Valid name required");
                }

                return _database.Insert(new Category { Name = name });
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
                return 0;
            }
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                return _database.Table<Category>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Category>();
        }

    }
}
