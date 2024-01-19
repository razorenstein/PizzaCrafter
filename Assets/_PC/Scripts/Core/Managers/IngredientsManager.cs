﻿using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using Assets._PC.Scripts.Core.Data.Resources;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.VFX;

namespace Assets._PC.Scripts.Core.Managers
{
    public class IngredientsManager
    {
        public Dictionary<IngredientType, IngredientData> Ingredients { get; private set; }
        private IngredientsConfig _config;

        public IngredientsManager()
        {
            Ingredients = new Dictionary<IngredientType, IngredientData>();
            _config = new IngredientsConfig();
            //PCManager.Instance.ConfigurationManager.GetConfig<IngredientsConfig>(OnConfigLoaded);
        }

        public bool TryGetIngredient(IngredientType type, int level, out IngredientData ingredientData)
        {
            ingredientData = null;
            if (_config.Ingredients.TryGetValue(type, out var ingredientDataConfig))
            {
                if (ingredientDataConfig.LevelData.TryGetValue(level, out var levelDataConfig))
                {
                    ingredientData = new IngredientData(levelDataConfig);
                    return true;
                }
            }

            return false;
        }

        public bool TryMergeIngredients(IngredientData ingredient1, IngredientData ingredient2, out IngredientData mergedIngredient)
        {
            mergedIngredient = null;

            if (ingredient1.IngredientType == ingredient2.IngredientType 
                && ingredient1.Level == ingredient2.Level
                && !IsMaxLevel(ingredient1))
            {
                if(_config.Ingredients.TryGetValue(ingredient1.IngredientType, out var ingredientDataConfig))
                {
                    if (ingredientDataConfig.LevelData.TryGetValue(ingredient1.Level + 1, out var levelDataConfig))
                    {
                        mergedIngredient = new IngredientData(levelDataConfig);

                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsResourceLevel(IngredientData ingredientData) => ingredientData.Level == 0;
        public bool IsMaxLevel(IngredientData ingredientData) => ingredientData.Level == ingredientData.MaxLevel;
        private void OnConfigLoaded(IngredientsConfig config) => _config = config;
    }
}