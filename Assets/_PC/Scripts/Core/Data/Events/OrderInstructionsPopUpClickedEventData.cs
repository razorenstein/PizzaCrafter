using Assets._PC.Scripts.Core.Data.Recipes;
using System;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class OrderInstructionsPopUpClickedEventData : PCBaseEventData
    {
        public Guid OrderId { get; set; }
        public RecipeData RecipeData { get; set; }
    }
}
