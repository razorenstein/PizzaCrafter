using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Resources.Config;
using Assets._PC.Scripts.Core.Data.Resources;
using System;
using System.Collections.Generic;
using Assets._PC.Scripts.Core.Data.Recipes;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using System.Diagnostics;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class RecipesManager
    {
        public Dictionary<ProductType, RecipeData> Recipes { get; private set; }
        private RecipesConfig _config;

        public RecipesManager()
        {
            Recipes = new Dictionary<ProductType, RecipeData>();
            _config = new RecipesConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<RecipesConfig>(OnConfigLoaded);
        }

        public bool GetRecipe(ProductType recipeType, out RecipeData recipe)
        {
            recipe = null;

            if (Recipes.TryGetValue(recipeType, out var recipeData))
            {
                recipe = recipeData;

                return true;
            }

            return false;
        }

        public bool CanCraftRecipe(List<IngredientLevelData> ingredients, out RecipeData recipeData)
        {
            recipeData = null;
            foreach (var recipe in Recipes.Values)
            {
                bool canCraft = true;
                foreach (var requiredIngredient in recipe.RequiredIngredients)
                {
                    // Check if the player has this required ingredient.
                    if (!ingredients.Contains(requiredIngredient))
                    {
                        canCraft = false;
                        break;
                    }
                }

                if (canCraft)
                {
                    recipeData = recipe;
                    return true;
                }
            }

            return false;
        }

        private void OnConfigLoaded(RecipesConfig config)
        {
            _config = config;           
            Recipes = _config.Recipes;
        }
    }
}
