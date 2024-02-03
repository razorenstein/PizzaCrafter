using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;

namespace Assets._PC.Scripts.Core.Managers
{
    public class IngredientsManager : IMerge
    {
        private IngredientsConfig _config;

        public IngredientsManager()
        {
            _config = new IngredientsConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<IngredientsConfig>(OnConfigLoaded);
        }

        public bool TryGetIngredient(IngredientType type, int level, out IngredientData ingredientData)
        {
            ingredientData = null;
            if (_config.IngredientLevels.TryGetValue(type, out var ingredientDataConfig))
            {
                if (ingredientDataConfig.LevelData.TryGetValue(level, out var levelDataConfig))
                {
                    ingredientData = new IngredientData(levelDataConfig);
                    return true;
                }
            }

            return false;
        }

        public bool TryMerge(TileData first, TileData second, out TileData merged)
        {
            merged = null;
            if (first is IngredientData firstIngredient && second is IngredientData secondIngredient)
            {
                if (firstIngredient.IngredientType == secondIngredient.IngredientType)
                {
                    if (_config.IngredientLevels.TryGetValue(firstIngredient.IngredientType, out var ingredientDataConfig))
                    {
                        if (ingredientDataConfig.LevelData.TryGetValue(firstIngredient.Level + 1, out var levelDataConfig))
                        {
                            merged = new IngredientData(levelDataConfig);

                            return true;
                        }
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
