using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace RecipeManagerXamarin
{
    [Table("cateogry")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<Recipe> Recipes { get; set; }
    }
}
