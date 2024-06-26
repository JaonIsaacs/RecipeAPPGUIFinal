using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAPPGUIFinal
{
    /// <summary>
    /// store data for ingredient
    /// </summary>
    public class Ingredient
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Quantity} {Unit})";
        }
    }
}
