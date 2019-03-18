using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace RecipeManagerXamarin
{
    public class RecipeManagerDatabase
    {
        // Stores a connection to the database.
        readonly SQLiteAsyncConnection _database;

        /// <summary>
        /// Constructor for the RecipeManagerDatabase class.
        /// </summary>
        /// <param name="dbPath">Path to the database.</param>
        public RecipeManagerDatabase(string dbPath)
        {
            // Creates a new connection to the database.
            _database = new SQLiteAsyncConnection(dbPath);

            // Creates the database tables.
            _database.CreateTableAsync<Category>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<Instruction>().Wait();
        }
    }
}
