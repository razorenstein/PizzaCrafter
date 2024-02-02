using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Oven
{
    public class OvenData : TileData
    {
        public int MaxCapacity { get;  set; }
        public OvenType OvenType { get; set; }
        public List<IngredientData> CurrentIngredients { get; set; } = new();
    }
}
