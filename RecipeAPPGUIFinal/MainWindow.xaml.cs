using System;
using System.Collections.ObjectModel;
using System.Linq;

using System.Windows;
using System.Windows.Controls;

namespace RecipeAPPGUIFinal
{


    public partial class MainWindow : Window
    {
        private RecipeManager recipeManager;
        private ObservableCollection<Recipe> displayedRecipes;


        /// <summary>
        /// main window functions
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            recipeManager = new RecipeManager();
            displayedRecipes = new ObservableCollection<Recipe>();
            lstFilteredRecipes.ItemsSource = displayedRecipes;
            cmbFoodGroupFilter.SelectionChanged += CmbFoodGroupFilter_SelectionChanged;
            txtIngredientFilter.TextChanged += TxtIngredientFilter_TextChanged;
            txtMaxProgramCaloriesFilter.TextChanged += TxtMaxProgramCaloriesFilter_TextChanged;
            txtMaxRecipeCaloriesFilter.TextChanged += TxtMaxRecipeCaloriesFilter_TextChanged;
        }

        private void BtnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string name = txtIngredientName.Text;
            string unit = txtUnit.Text;
            double quantity;

            if (double.TryParse(txtQuantity.Text, out quantity))
            {
                lstOriginalIngredients.Items.Add(new Ingredient
                {
                    Name = name,
                    Unit = unit,
                    Quantity = quantity
                });

                ///<summary>
                ///clear alter entering ingredient 
                /// </summary>
                txtIngredientName.Text = "";
                txtUnit.Text = "";
                txtQuantity.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }


        /// <summary>
        /// saves recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            var ingredients = lstOriginalIngredients.Items.Cast<Ingredient>().ToList();

            int calories;
            if (!int.TryParse(txtCalories.Text, out calories))
            {
                MessageBox.Show("Please enter a valid number of calories.");
                return;
            }

            Recipe newRecipe = new Recipe
            {
                Name = txtRecipeName.Text,
                Steps = txtRecipeSteps.Text,
                FoodGroup = txtFoodGroup.Text,
                Calories = calories,
                Ingredients = ingredients
            };

            recipeManager.AddRecipe(newRecipe);

           
            displayedRecipes.Clear();
            foreach (var recipe in recipeManager.Recipes.OrderBy(r => r.Name))
            {
                displayedRecipes.Add(recipe);
            }

            
            txtRecipeName.Text = "";
            txtRecipeSteps.Text = "";
            txtFoodGroup.Text = "";
            txtCalories.Text = "";
            lstOriginalIngredients.Items.Clear();
        }

        private void BtnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeNameToDelete = txtRecipeNameToDelete.Text;

            var recipeToDelete = recipeManager.Recipes.FirstOrDefault(r => r.Name.Equals(recipeNameToDelete, StringComparison.OrdinalIgnoreCase));

            if (recipeToDelete != null)
            {
                recipeManager.Recipes.Remove(recipeToDelete);

                // Update displayed recipes and clear input fields
                displayedRecipes.Clear();
                foreach (var recipe in recipeManager.Recipes.OrderBy(r => r.Name))
                {
                    displayedRecipes.Add(recipe);
                }

                MessageBox.Show($"Recipe '{recipeNameToDelete}' deleted successfully.");
            }
            else
            {
                MessageBox.Show($"Recipe '{recipeNameToDelete}' not found.");
            }
        }

       
        /// <summary>
        /// filters for user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtIngredientFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void CmbFoodGroupFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void TxtMaxProgramCaloriesFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void TxtMaxRecipeCaloriesFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filteredRecipes = recipeManager.Recipes.Where(r =>
                (string.IsNullOrEmpty(txtIngredientFilter.Text) || r.Ingredients.Any(ig => ig.Name.Contains(txtIngredientFilter.Text))) &&
                (cmbFoodGroupFilter.SelectedItem == null || r.FoodGroup.Equals(cmbFoodGroupFilter.SelectedItem.ToString())) &&
                (string.IsNullOrEmpty(txtMaxProgramCaloriesFilter.Text) || r.Calories <= int.Parse(txtMaxProgramCaloriesFilter.Text)) &&
                (string.IsNullOrEmpty(txtMaxRecipeCaloriesFilter.Text) || r.Calories <= int.Parse(txtMaxRecipeCaloriesFilter.Text))
            ).OrderBy(r => r.Name).ToList();

            displayedRecipes.Clear();
            foreach (var recipe in filteredRecipes)
            {
                displayedRecipes.Add(recipe);
            }
        }

        private void LstFilteredRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFilteredRecipes.SelectedItem is Recipe selectedRecipe)
            {
                txtOriginalRecipeName.Text = selectedRecipe.Name;
                txtOriginalRecipeSteps.Text = selectedRecipe.Steps;
                txtOriginalFoodGroup.Text = selectedRecipe.FoodGroup;
                txtOriginalCalories.Text = selectedRecipe.Calories.ToString();

                lstOriginalIngredientsToDelete.Items.Clear();
                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    lstOriginalIngredientsToDelete.Items.Add(ingredient);
                }
            }
        }
    }
}



