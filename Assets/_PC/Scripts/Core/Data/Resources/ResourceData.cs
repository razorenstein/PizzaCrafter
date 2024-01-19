using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Resources
{
    public class ResourceData : TileData
    {
        public string Name { get; set; }
        public ResourceType ResourceType { get; set; }
        public IngredientType IngredientType { get; set; }
    }
}
