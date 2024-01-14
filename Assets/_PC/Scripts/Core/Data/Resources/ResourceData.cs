using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Resources
{
    public class ResourceData
    {
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public IngredientType IngredientType { get; set; }
        public string SpriteAddressableKey { get; set; }
    }
}
