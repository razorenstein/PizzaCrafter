using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;

namespace Assets._PC.Scripts.Core.Data.Resources
{
    public class ResourceData : TileData
    {
        public ResourceType ResourceType { get; set; }
        public IngredientType IngredientType { get; set; }
    }
}
