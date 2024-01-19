
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;

namespace Assets._PC.Scripts.Core.Data.Ingredients.Abstract
{
    public class IngredientData : TileData
    {
        public IngredientType IngredientType { get; private set; }
        public int Level { get; private set; }
        public int MaxLevel { get; private set; }

        public IngredientData(IngredientLevelData ingredientConfig)
        {
            IngredientType = ingredientConfig.Type;
            Level = ingredientConfig.Level;
            MaxLevel = ingredientConfig.MaxLevel;
            SpriteAddressableKey = ingredientConfig.SpriteAddressableKey;
        }
    }
}
