using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using Assets._PC.Scripts.Core.Data.Resources;
using Assets._PC.Scripts.Core.Data.Resources.Config;
using System.Collections.Generic;

namespace Assets._PC.Scripts.Core.Managers
{
    public class ResourceManager
    {
        public Dictionary<ResourceType, ResourceData> Resources { get; private set; }
        private ResourcesConfig _config;

        public ResourceManager()
        {
            Resources = new Dictionary<ResourceType, ResourceData>();
            _config = new ResourcesConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<ResourcesConfig>(OnConfigLoaded);
        }

        public bool TryLootResource(ResourceType resourceType)
        {
            if (Resources.TryGetValue(resourceType, out var resourceData))
            {
                if(PCManager.Instance.IngredientsManager.TryGetIngredient(resourceData.IngredientType, 1, out var ingredientData))
                {
                    if (PCManager.Instance.BoardManager.TrySetTileRandomally(ingredientData))
                        return true;
                }
            }

            return false;
        }

        private void OnConfigLoaded(ResourcesConfig config)
        {
            _config = config;
            Resources = _config.Resources;
        }
    }
}