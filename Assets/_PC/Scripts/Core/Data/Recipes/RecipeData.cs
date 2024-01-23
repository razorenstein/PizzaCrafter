using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Recipes
{
    public class RecipeData
    {
        public RecipeType Type { get; set; }
        public IngredientData[] RequiredIngredients { get; set; }
        public float CraftingTime { get; set; }
        //public CraftedItem ResultItem { get; set; }
    }
}
