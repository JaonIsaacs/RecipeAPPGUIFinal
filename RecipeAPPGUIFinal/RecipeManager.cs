using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAPPGUIFinal
{

    /// <summary>
    /// manages recipe being added to array
    /// </summary>
    public class RecipeManager
    {
        public List<Recipe> Recipes { get; set; }

        public RecipeManager()
        {
            Recipes = new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }
    }
}

