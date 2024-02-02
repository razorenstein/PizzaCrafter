using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Oven;
using Assets._PC.Scripts.Core.Data.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class OvenIngredientsFormedRecipe : PCBaseEventData
    {
        public OvenData Oven { get; set; }
        public RecipeData Recipe { get; set; }
        public IngredientData Ingredient { get; set; }
    }
}
