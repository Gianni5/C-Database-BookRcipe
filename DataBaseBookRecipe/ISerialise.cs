using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBookRecipe
{
    interface ISerialise
    {
        void SerialisedFave(List<Recipe> faveRecipe);

        List<Recipe> DeserialisedFave();
    }
}
