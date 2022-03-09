using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DataBaseBookRecipe
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        // connection string to the database
        CookContex context = new CookContex(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=recipesBook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        // declearing all the lists we are going to work with
        List<Recipe> recipes = new List<Recipe>();
        List<IngredientCollection> ingredientCollections = new List<IngredientCollection>();
        List<Ingredient> ingredients = new List<Ingredient>();
        List<RecipeMethod> recipeMethods = new List<RecipeMethod>();
        List<string> addedIngredient = new List<string>();
        public MainWindow()
        {

            InitializeComponent();
            // get all the ingredients store in database ingredient table
            ingredients = context.GetAllIngredient();
            // adding ingredients from the ingredients table to the comboBox (AddIngrComBox)
            AddIngrComBox.ItemsSource = ingredients;
            // get all the recipes store in database recipe table
            recipes = context.GetAllRecipes();

            foreach (Recipe recipe in recipes)
            {
                RecipeList.ItemsSource = recipes;
            }




        }
        /// <summary>
        ///  listbox (RecipeList) where items get selected and creating events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // clear the IngridientLstView from the items
            IngridientListView.Items.Clear();
            // selecting recipe listed in recipelist
            Recipe selectedRecipe = (Recipe)RecipeList.SelectedItem;

            if (selectedRecipe == null) return;
            {


            }
            // get the ingredients and quantity related to the selectedRecipe and display them in ingredientLsitView
            ingredientCollections = context.GetIngredients(selectedRecipe);
            

            // adding new ingredient to the selected recipes 
            foreach (IngredientCollection ingredient in ingredientCollections)
            {
                IngridientListView.Items.Add(ingredient);
            }
            // showing the steps and methods (StepsTexBo) of selected recipe when selected
            if (selectedRecipe != null)
            {
                StepsTexBo.Text = selectedRecipe.TimeSteps;
            }


            StarRecipe();


        }
        /// <summary>
        /// adding new recipes to the database book recipe when all the requirement are filled and the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            
            bool canAdd = false;
            foreach (var item in recipes)
            {
                // cheching if the new inserted recipe already exist in upper and lowercase
                if (item.RecipeName.ToLower() == RecipeAddBox.Text.ToLower())
                {
                    canAdd = false;
                    break;
                }
                else
                {
                    canAdd = true;
                }
            }
            // if the boolen value is true the new recipe and steps are added to the list
            if (canAdd)
            {

                recipes.Add(new Recipe
                {
                    RecipeName = RecipeAddBox.Text,
                    TimeSteps = StepsTexBo.Text
                });

                RecipeList.ItemsSource = null;
                RecipeList.ItemsSource = recipes;

                if (RecipeList.ItemsSource != null)
                {
                    RecipeList.ItemsSource = recipes;
                }

                // add recipe name and steps to the recipe database
                Recipe r = new Recipe(RecipeAddBox.Text, StepsTexBo.Text);
                context.AddRecipe(r);

                recipes = context.GetAllRecipes();
                RecipeList.ItemsSource = null;
                RecipeList.ItemsSource = recipes;
            }
            else
            {
                // when the inserted recipe already exist
                MessageBox.Show("Recipe Already Exist");
            }

        }
        private void StepsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        ///update the recipe method table with new ingredients and quantities to the selected recipe 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
           context.AddIngrQty(recipeMethods);
        }

        private void AddIngrComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// creating a list of favorite recipes when the starbutton is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            
            updateUI();
            
            // creating a new favelist 
            List<Recipe> faveList = new List<Recipe>();
            
            // checking every recipe in the in recipes list   
            foreach (var item in recipes)
            {
                //if the boolen value Favourite is true
                if (item.Favourite)
                {
                    // a new favorite recipe is added
                    faveList.Add(new Recipe
                    {
                        RecipeName = item.RecipeName,
                        Favourite = item.Favourite,
                        ID = item.ID,
                        TimeSteps = item.TimeSteps
                    });
                }
                else
                {
                    // context.DeserialisedFave();                            ASK ARON
                }
            }
            // creating a text binary file with a list of favorite recipes
            context.SerialisedFave(faveList);
        }

        /// <summary>
        /// updating the the star button img and displaing a message if the recipe is not selected
        /// </summary>
        private void updateUI()
        {
            Recipe selectedRecipe = (Recipe)RecipeList.SelectedItem;
            MediaButton.Content = FindResource("SOff");
            
            if (selectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe first!");
                return;


            }
            selectedRecipe.Favourite = !selectedRecipe.Favourite;

            StarRecipe();
        }

        private void QtyAddBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// adding ingredients and quantity to the selected recipe but not to the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddIngQty_Click(object sender, RoutedEventArgs e)
        {
            // if the recipe is not selected a message will display 
            if (RecipeList.SelectedIndex == -1)
            {

                MessageBox.Show("Please selecet recipe first!");
                return;
            }
            // if the ingredient is not selected a message will display 
            if (AddIngrComBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please selecet ingredient!");
                return;
            }

            // if ingredient is not selected return null
            if (AddIngrComBox.SelectedItem == null)
            {
                return;
            }
            IngridientListView.ItemsSource = null;
            
            // check if the QtyAddBox is empty and display a message when we try to add ingredients without quantity
            if (string.IsNullOrWhiteSpace(QtyAddBox.Text))
            {
                MessageBox.Show("please insert quantity!");
                AddIngrComBox.SelectedItem = null;
            }
            else
            {
                // adding ingredients when the quantity of the selected ingredient is added
                IngridientListView.Items.Add(AddIngrComBox.SelectedItem.ToString() + ", " + QtyAddBox.Text);

            }

            // adding new qty, ingredientID, and recipeID to the the RecipeMethod list
            recipeMethods.Add(new RecipeMethod
            {
                Qty = QtyAddBox.Text,
                IngridientID = ingredients[AddIngrComBox.SelectedIndex].ID,
                RecipeID = recipes[RecipeList.SelectedIndex].ID,

            });

            // adding a new ingredient to existing ingredients of a selected recipe
            if (IngridientListView.Items.Count > 0)
            {
                foreach (var item in IngridientListView.Items)
                {
                    addedIngredient.Add(item.ToString());

                }
            }

            // empty the quantity box
            QtyAddBox.Text = null;
        }

        /// <summary>
        /// deleted a selected recipe and all the related values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            Recipe selectedrecipe = (Recipe)RecipeList.SelectedItem;
            

            if (selectedrecipe == null)
            {
                MessageBox.Show("please select a recipe you wish to delete!");
                
                return;
            }

            // crating a new istance sr and give the value of the selected recipe in the database
            Recipe sr = context.Recipes.FirstOrDefault(recipe => recipe.ID == selectedrecipe.ID);
            // deleting the selected recipe
            context.Recipes.DeleteOnSubmit(sr);
            // update the changes  
            context.SubmitChanges();
            // clean the recipelist 
            RecipeList.SelectedItem = null;
            recipes = context.GetAllRecipes();
            // clean the steps texBox
            StepsTexBo.Text = "";
            RecipeList.SelectedItem = null;
            RecipeList.ItemsSource = recipes;

        }
        /// <summary>
        /// changing the star img if the selected recipe is favorite 
        /// </summary>
        private void StarRecipe()
        {   
            Recipe selectedRecipe = (Recipe)RecipeList.SelectedItem;

            if (selectedRecipe.Favourite)
            {
                MediaButton.Content = FindResource("SOn");
            }
            else
            {
                MediaButton.Content = FindResource("SOff");
            }

        }

        private void StepsTexBo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /*
         this goes into save to binary file button 
         context.SerialisedRecipe(myFave);
         */
    }
}
