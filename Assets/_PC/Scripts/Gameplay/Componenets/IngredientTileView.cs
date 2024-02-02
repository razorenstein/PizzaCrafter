using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class IngredientTileView : TileView, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsDraggable)
            {
                var ingredientData = (IngredientData)Data;
                Manager.OvenManager.RemoveFromOven(ingredientData);
                SetDraggable();
            }
        }
    }
}
