using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBookRecipe
{
    [Serializable]
    [Table]
    public class Recipe
    {
        [Column(IsPrimaryKey =true, IsDbGenerated = true, DbType = "INT IDENTITY")]
        public int ID { get; set; }
        [Column]
        public string RecipeName { get; set; }
        [Column]
        public string TimeSteps { get; set; }

        public bool Favourite { get; set; }
        //public List<Ingredient> Ingredients { get; set; }

        public Recipe()
        {

        }

        public Recipe(string recipeName, string timeSteps) //, List<Ingredient> ingredients = null)
        {
            //Ingredients = ingredients;

            RecipeName = recipeName;

            TimeSteps = timeSteps;

            // Ingredients = (ingredients == null) ? new List<Ingredient>() : ingredients; //null check or assign if not null
        }

        //public List<Ingredient> Ingredients { get; set; }

        public override string ToString()
        {
            return $"{RecipeName} ";
        }
    }
}
