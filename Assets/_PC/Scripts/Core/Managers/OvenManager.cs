using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Mappers;
using Assets._PC.Scripts.Core.Data.Oven;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class OvenManager
    {
        public List<OvenData> Ovens { get; private set; }
        private OvensConfig _config;

        public OvenManager()
        {
            Ovens = new List<OvenData>();
            _config = new OvensConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<OvensConfig>(OnConfigLoaded);
        }

        public bool TrySetToOven(OvenData oven, IngredientData ingredient)
        {
            if (oven.MaxCapacity > oven.CurrentIngredients.Count)
            {
                oven.CurrentIngredients.Add(ingredient);

                if (PCManager.Instance.RecipesManager.CanCraftRecipe(oven.CurrentIngredients.Select(i => i.Map()).ToList(), out var recipe))
                {
                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnOvenIngredientsFormRecipe, new OvenIngredientsFormedRecipe()
                    {
                        Oven = oven,
                        Ingredient = ingredient,
                        Recipe = recipe
                    });
                }

                PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnIngredientMovedToOven, new IngredientMovedToOven()
                {
                    Oven = oven,
                    Ingredient = ingredient
                });

                return true;
            }

            return false;
        }

        public bool TryProduceProduct(OvenData oven)
        {
            if (PCManager.Instance.RecipesManager.CanCraftRecipe(oven.CurrentIngredients.Select(i => i.Map()).ToList(), out var recipe))
            {
                if (PCManager.Instance.ProductManager.TryGetProduct(recipe.ProductType, 1, out var productData))
                {
                    if (PCManager.Instance.BoardManager.TrySetTileRandomally(productData))
                    {
                        PCManager.Instance.BoardManager.RemoveTiles(oven.CurrentIngredients.Select(i => i as TileData).ToList());
                        oven.CurrentIngredients.Clear();

                        return true;
                    }
                }
            }

            return false;
        }

        public void RemoveFromOven(IngredientData ingredient)
        {
            foreach (var oven in Ovens)
            {
                oven.CurrentIngredients.RemoveAll(i => i.Id == ingredient.Id);
            }
        }

        private void OnConfigLoaded(OvensConfig config)
        {
            _config = config;
            Ovens.Add(_config.Ovens.Values.First());
        }
    }
}
