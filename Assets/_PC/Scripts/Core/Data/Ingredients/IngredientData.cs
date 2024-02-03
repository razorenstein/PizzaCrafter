
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;

namespace Assets._PC.Scripts.Core.Data.Ingredients.Abstract
{
    public class IngredientData : TileData
    {
        public IngredientType IngredientType { get; private set; }

        public IngredientData(IngredientLevelData ingredientLevelConfig) : base()
        {
            IngredientType = ingredientLevelConfig.Type;
            Level = ingredientLevelConfig.Level;
            MaxLevel = ingredientLevelConfig.MaxLevel;
            SpriteAddressableKey = ingredientLevelConfig.SpriteAddressableKey;
            Type = TileType.Ingredient;
        }
    }
}
