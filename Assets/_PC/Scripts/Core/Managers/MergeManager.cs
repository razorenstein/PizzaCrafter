using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Managers
{
    public class MergeManager : IMerge
    {
        public MergeManager()
        {
        }

        public bool TryMerge(TileData first, TileData second, out TileData merged)
        {
            merged = null;
            if (first.Type != second.Type || first.Level != second.Level|| IsMaxLevel(first))
                return false;

            switch (first.Type)
            {
                case TileType.Ingredient:
                    if (PCManager.Instance.IngredientsManager.TryMerge(first, second, out TileData mergedIngredient))
                    {
                        merged = (IngredientData) mergedIngredient;
                        return true;
                    }
                    break;
            }

            return false;
        }

        private bool IsMaxLevel(TileData tile) => tile.Level == tile.MaxLevel;
    }
}
