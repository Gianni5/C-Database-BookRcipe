using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBookRecipe
{
    class IngredientCollection
    {
        public int IngredientId { get; set; }
        public string IngredientName{get;set;}
        public string IngredientQty{get;set;}

        

        public override string ToString()
        {
            return $"{IngredientName} , {IngredientQty}";
        }
    }

}
