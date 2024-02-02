using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Recipes
{
    public class RecipeData
    {
        public ProductType ProductType { get; set; }
        public IngredientLevelData[] RequiredIngredients { get; set; }
        public float CraftingTime { get; set; }
    }
}
