using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace RecipeManagerXamarin
{
    public class Instruction
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
        public string InstructionCount { get; set; }

        [NotNull]
        public string TotalDuration { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<Instruction> Instructions { get; set; }
    }
}
