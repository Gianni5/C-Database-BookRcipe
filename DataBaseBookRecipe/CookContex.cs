using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataBaseBookRecipe
{
    class CookContex : DataContext, ISerialise 
    {
        public Table<Recipe> Recipes;
        public Table<Ingredient> Ingredients;
        public Table<RecipeMethod> RecipeMethods;

        public CookContex(string con) : base(con)
        {  
           //if we wnat keep the new added recipes simply commented out InitData(); below 
           InitData();

        }

        /// <summary>
        /// this method will create and delete database every time we run the programm
        /// </summary>
        private void InitData()
        {
            //if (!DatabaseExists()) return;
            DeleteDatabase();
            CreateDatabase();

            // recipes name and steps for the existing recipes
            List<Recipe> recipes = new List<Recipe>()
            {
                new Recipe("Spaghetti Alla Bolognese"," Directions Preparation:25min  ›  Cook:45min  ›  Ready in:1hour10min "+ "\n" +
                    "1 - Heat the oil in a large saucepan, add the onion, carrot, celery, garlic and sundried tomatoes, and fry for 5–10 minutes" + "\n" +
                    "2 - Stirring frequently, until the vegetables start to brown." + "\n" + " Add the minced beef and fry, stirring, until the meat is browned" + "\n" +
                    "3 - Pour in the wine, the tomatoes with their juice and the beef stock. " + "\n" + " Stir in the herbs and pepper to season" + "\n" +
                    "4 - Cover the pan and simmer for 30 minutes, stirring occasionally. " + "\n" + " Meanwhile, cook the spaghetti in boiling water for 10–12 minutes" + "\n" +
                    "5 - Drain the spaghetti and mix it with the meat sauce, tossing until the strands are well coated" + "\n" +
                    "6 - Sprinkle with Parmesan cheese and serve at once."),
                new Recipe("Polpette"," Directions Preparation:25min  ›  Cook: 20min  ›  Ready in:45min " + "\n" +
                    "1 - Heat oven to 400°F. Line 13x9-inch pan with foil; spray with cooking spray." + "\n" +
                    "2 - In large bowl, mix all ingredients. " + "\n" + " Shape mixture into 24(1 1 / 2 - inch) meatballs.Place 1 inch apart in pan." + "\n" +
                    "3 - Bake uncovered 18 to 22 minutes or until temperature reaches 160°F and no longer pink in center."),
                new Recipe("Carbonara"," Directions Preparation: 10min  ›  Cook: 10min  ›  Ready in:20min " + "\n" +
                    "1 - In a large frying pan add the oil , pancetta and hot pepper flakes " + "\n" + " (if using) cook on medium heat until the pancetta is cooked(but not too crispy)."+"\n"+" Stirring often so the pancetta doesn’t burn." + "\n" +
                    "2 - While pancetta is cooking , boil a large pot of water, " + "\n" + " when the water has boiled add some salt and the pasta and cook until al dente." + "\n" +
                    "3 - While pasta is cooking, in a small bowl beat the 3 eggs,  " + "\n" + " then add the parmesan and mix very well." + "\n" +
                    "4 - When the pasta is cooked turn the heat back on the pancetta(to medium high), " + "\n" + " add the drained pasta toss together to combine well for about 20 - 30 seconds." + "\n" +
                    "5 - Remove the pan from the heat add the egg mixture, constantly tossing together, " + "\n" + " add a tablespoon or two of pasta water to make sure it is very creamy and continue to toss until well blended." + "\n" +
                    "6 - Top with parmesan cheese if desired.Serve immediately.")

            };
            Recipes.InsertAllOnSubmit(recipes);
            SubmitChanges();
           
            // list of existing ingredients
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient("spaghetti"),
                new Ingredient("tomato sauce"),
                new Ingredient("garlic"),
                new Ingredient("chilli"),
                new Ingredient("olive oil"),
                new Ingredient("oil"),
                new Ingredient("salt"),
                new Ingredient("pepper"),
                new Ingredient("chees"),
                new Ingredient("tomato"),
                new Ingredient("chicken"),
                new Ingredient("potato"),
                new Ingredient("onion"),
                new Ingredient("celery"),
                new Ingredient("egg"),
                new Ingredient("pancetta"),
                new Ingredient("bread"),
                new Ingredient("water"),
                new Ingredient("mince beef"),
                new Ingredient("carrot"),
                new Ingredient("mussels"),
                new Ingredient("black olives"),
                new Ingredient("cappers"),
            };
            Ingredients.InsertAllOnSubmit(ingredients);
            SubmitChanges();


            // list of existing recipes with ingredients and quantity needed
            List<RecipeMethod> recipeMethods = new List<RecipeMethod>()
            {
                new RecipeMethod(1,1," 500g"),
                new RecipeMethod(1,2," 500g"),
                new RecipeMethod(1,3," 1 clove"),
                new RecipeMethod(1,5,"10 ml"),
                new RecipeMethod(1,7," pinch"),
                new RecipeMethod(1,8," pinch"),
                new RecipeMethod(1,13,"1"),
                new RecipeMethod(1,19,"400g"),
                new RecipeMethod(1,9," as like"),
                new RecipeMethod(1,20,"1 carrot"),
                new RecipeMethod(1,14,"2 sticks"),

                new RecipeMethod(2,19,"500g"),
                new RecipeMethod(2,17,"150g"),
                new RecipeMethod(2,3," 1 clove"),
                new RecipeMethod(2,2," 200g"),
                new RecipeMethod(2,5," 100g"),
                new RecipeMethod(2,7," pinch"),
                new RecipeMethod(2,8," pinch"),

                new RecipeMethod(3,1, "500g"),
                new RecipeMethod(3,5, "10 ml"),
                new RecipeMethod(3,7, "pinch"),
                new RecipeMethod(3,8, "pinch"),
                new RecipeMethod(3,9, "as like"),
                new RecipeMethod(3,15, "3 whole"),
                new RecipeMethod(3,16, "150 g")




            };
            RecipeMethods.InsertAllOnSubmit(recipeMethods);
            SubmitChanges();
        }



        /// <summary>
        /// gets all the recipes in recipe table
        /// </summary>
        /// <returns></returns>
        public List<Recipe> GetAllRecipes()
        {

            return (from r in Recipes.AsEnumerable()
                    select r).ToList();

        }
        
        /// <summary>
        /// get all the ingredient in ingredient table
        /// </summary>
        /// <returns></returns>
        public List<Ingredient> GetAllIngredient()
        {

            return (from i in Ingredients.AsEnumerable()
                    select i).ToList();

        }

        /// <summary>
        /// get all existing recipe ID, ingridient ID and quantity in RecipeMethod table 
        /// </summary>
        /// <returns></returns>
        public List<RecipeMethod> GetAllRecipeMethods()
        {
            return (from rm in RecipeMethods.AsEnumerable()
                    select rm).ToList();

        }
        /// <summary>
        /// joing the tables on ID 
        /// </summary>
        /// <param name="r"></param>
        /// <returns> a new list <ingredientCollection> with joing ID </returns>
        public List<IngredientCollection> GetIngredients(Recipe r)
        {
            var ing = from i in Ingredients
                      join RecipeMethods in RecipeMethods on i.ID equals RecipeMethods.IngridientID
                      where RecipeMethods.RecipeID == r.ID
                      select new IngredientCollection
                      {
                          IngredientName = i.IngName,
                          IngredientQty = RecipeMethods.Qty,
                          IngredientId = i.ID,

                      };


            return ing.ToList();


        }
        /// <summary>
        /// adding recipe name
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool AddRecipe(Recipe r)
        {
            // add record for recipe
            Recipes.InsertOnSubmit(r);

            SubmitChanges();
            Recipe insertedRecipe = Recipes.FirstOrDefault(recipe => recipe.RecipeName == r.RecipeName);
            return true;

            
        }
        /// <summary>
        /// adding  quantity and ingredient to recipes 
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        public bool AddIngrQty(List<RecipeMethod> rm)
        {

             
            foreach (var ingridient in rm)
            {   
                //if the ingridient already exist dont add
                if (RecipeMethods.FirstOrDefault(ing => ing.IngridientID == ingridient.IngridientID && ing.RecipeID == ingridient.RecipeID) == null)
                {
                    RecipeMethods.InsertOnSubmit(ingridient);
                }
            }
            
            SubmitChanges();
            return true;

        }
        /// <summary>
        /// creating a new binary text file for reading and writing in bin folder, if the file already exists it will overwritten
        /// </summary>
        /// <param name="faveRecipe"></param>
        public void SerialisedFave(List<Recipe> faveRecipe)
        {
            Stream file = File.Open("faveRecipe.bin", FileMode.Create);
            //try catch exeption when an exernal file been open 
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(file, faveRecipe);
                


            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                file.Close();
            }

        }

        /// <summary>
        /// deserialize the specific file end returing faveRecipe list
        /// </summary>
        /// <returns></returns>
        public List<Recipe> DeserialisedFave()
        {
            Stream file = File.Open("faveRecipe.bin", FileMode.Open);
            List<Recipe> faveRecipe;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                faveRecipe = (List<Recipe>)binaryFormatter.Deserialize(file);

                return faveRecipe;

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                file.Close();
            }
            
            
        }
    }
}