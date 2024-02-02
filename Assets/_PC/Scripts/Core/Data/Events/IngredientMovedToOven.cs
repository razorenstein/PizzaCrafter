using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Oven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class IngredientMovedToOven : PCBaseEventData
    {
        public OvenData Oven { get; set; }
        public IngredientData Ingredient { get; set; }
    }
}
