using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Resources;
using System.Collections.Generic;

namespace Assets._PC.Scripts.Core.Managers
{
    public class ResourceManager
    {
        public Dictionary<ResourceType, ResourceData> Resources { get; private set; }
        //resoucesfromconfig

        public ResourceManager()
        {
            Resources = new Dictionary<ResourceType, ResourceData>();
            Initialize();
        }

        private void Initialize()
        {
            var milkSack = new ResourceData()
            {
                Name = "Milk Bag",
                ResourceType = ResourceType.MilkBag,
                Type = TileType.Resource,
                IngredientType = IngredientType.Cheese,
                SpriteAddressableKey = "milk_resource"
            };
            var tomatoSack = new ResourceData()
            {
                Name = "Tomato Sack",
                ResourceType = ResourceType.TomatoSack,
                Type = TileType.Resource,
                IngredientType = IngredientType.Tomato,
                SpriteAddressableKey = "tomato_resource"
            };
            var flourSack = new ResourceData()
            {
                Name = "Flour Sack",
                ResourceType = ResourceType.FlourSack,
                Type = TileType.Resource,
                IngredientType = IngredientType.Flour,
                SpriteAddressableKey = "flour_resource"

            };
            Resources.Add(ResourceType.MilkBag, milkSack);
            Resources.Add(ResourceType.TomatoSack, tomatoSack);
            Resources.Add(ResourceType.FlourSack, flourSack);

            PCManager.Instance.BoardManager.TrySetTileRandomally(milkSack);
            PCManager.Instance.BoardManager.TrySetTileRandomally(tomatoSack);
            PCManager.Instance.BoardManager.TrySetTileRandomally(flourSack);

        }

        public bool TryLootResource(ResourceType resourceType)
        {
            if (Resources.TryGetValue(resourceType, out var resourceData))
            {
                if(PCManager.Instance.IngredientsManager.TryGetIngredient(resourceData.IngredientType, 0, out var ingredientData))
                {
                    if (PCManager.Instance.BoardManager.TrySetTileRandomally(ingredientData))
                        return true;
                }
            }

            return false;
        }
    }
}