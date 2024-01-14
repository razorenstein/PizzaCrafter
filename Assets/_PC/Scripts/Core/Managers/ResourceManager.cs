using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Ingredients;
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
                Type = ResourceType.MilkBag,
                IngredientType = IngredientType.Cheese,
                SpriteAddressableKey = "milk_resource"
            };
            var tomatoSack = new ResourceData()
            {
                Name = "Tomato Sack",
                Type = ResourceType.TomatoSack,
                IngredientType = IngredientType.Tomato,
                SpriteAddressableKey = "tomato_resource"
            };
            var flourSack = new ResourceData()
            {
                Name = "Flour Sack",
                Type = ResourceType.FlourSack,
                IngredientType = IngredientType.Flour,
                SpriteAddressableKey = "flour_resource"

            };
            Resources.Add(ResourceType.MilkBag, milkSack);
            Resources.Add(ResourceType.TomatoSack, tomatoSack);
            Resources.Add(ResourceType.FlourSack, flourSack);
        }

        public bool TryGetResourceLoot(ResourceType resourceType)
        {
            if (Resources.TryGetValue(resourceType, out var resourceData))
            {
                var itemToProduce = new IngredientData
                {
                    Type = resourceData.IngredientType,
                    SpriteAddressableKey = resourceData.IngredientType switch
                    {
                        IngredientType.Flour => "ingredient-flour-1",
                        IngredientType.Cheese => "ingredient-cheese-1",
                        IngredientType.Tomato => "ingredient-tomato-1",
                        _ => throw new System.NotImplementedException(),
                    },
                    Level = 1
                };

                if (PCManager.Instance.BoardManager.TrySetTile(itemToProduce))
                    return true;

            }

            return false;
        }
    }
}