using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBookRecipe
{
    [Table]
    public class Ingredient
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT IDENTITY")]
        public int ID { get; set; }

        [Column]
        public string IngName { get; set; }

        public Ingredient()
        {

        }

        public Ingredient(string name )
        {
            IngName = name;
           
        }

        public List<Recipe> Recipes { get; set; }

        public override string ToString()
        {
            return $"{IngName}";
        }
    }
}
