using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace RecipeManagerXamarin
{
    [Table("instruction")]
    public class Instruction
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull, ForeignKey(typeof(Recipe))]
        public int RecipeID { get; set; }

        [NotNull]
        public int SequenceNumber { get; set; }

        [NotNull]
        public string Description { get; set; }
    }
}
