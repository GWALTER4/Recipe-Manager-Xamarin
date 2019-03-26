using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace RecipeManagerXamarin
{
    [Table("recipe")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull, ForeignKey(typeof(Category))]
        public int CategoryID { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        [NotNull]
        public string IngredientsList { get; set; }

        [NotNull]
        public int InstructionCount { get; set; }

        [NotNull]
        public int TotalDuration { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Instruction> Instructions { get; set; }
    }
}
