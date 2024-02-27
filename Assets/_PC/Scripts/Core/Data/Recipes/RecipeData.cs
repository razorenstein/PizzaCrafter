using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;

namespace Assets._PC.Scripts.Core.Data.Recipes
{
    public class RecipeData
    {
        public ProductType ProductType { get; set; }
        public IngredientLevelData[] RequiredIngredients { get; set; }
        public float CraftingTime { get; set; }
        public string SpriteAddressableKey { get; set; }
    }
}
