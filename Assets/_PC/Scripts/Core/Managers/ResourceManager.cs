using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ItemType = ItemType.Cheese
            };
            var tomatoSack = new ResourceData()
            {
                Name = "Tomato Sack",
                Type = ResourceType.TomatoSack,
                ItemType = ItemType.Cheese
            };
            var flourSack = new ResourceData()
            {
                Name = "Flour Sack",
                Type = ResourceType.FlourSack,
                ItemType = ItemType.Cheese
            };
            Resources.Add(ResourceType.MilkBag, milkSack);
            Resources.Add(ResourceType.TomatoSack, tomatoSack);
            Resources.Add(ResourceType.FlourSack, flourSack);
        }

        public bool TryGetResourceLoot(ResourceType resourceType)
        {
            if (Resources.TryGetValue(resourceType, out var resourceData))
            {
                var itemToProduce = new ItemData
                {
                    Type = resourceData.ItemType,
                    Level = 0
                };

                if (PCManager.Instance.BoardManager.TrySetTile(itemToProduce))               
                    return true;
                
            }

            return false;        
        }
    }
}