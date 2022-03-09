using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBookRecipe
{
    [Table]
    class RecipeMethod
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT IDENTITY")]
        public int ID { get; set; }
        [Column]
        public int RecipeID { get; set; }
        [Column]
        public int IngridientID { get; set; }
        [Column]
        public string Qty { get; set; }
        
        public RecipeMethod()
        {

        }

        public RecipeMethod(int recipeID, int ingredientID, string qty )
        {
            RecipeID = recipeID;
            IngridientID = ingredientID;
            Qty = qty;
            
        }
    }
}
