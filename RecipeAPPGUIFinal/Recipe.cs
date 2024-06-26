using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAPPGUIFinal
{

    /// <summary>
    /// store recipe info
    /// </summary>
    public class Recipe
    {
        public string Name { get; set; }
        public string Steps { get; set; }
        public string FoodGroup { get; set; }
        public int Calories { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
