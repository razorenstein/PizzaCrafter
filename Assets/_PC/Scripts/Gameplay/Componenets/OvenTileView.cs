using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Oven;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class OvenTileView : TileView, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Manager.OvenManager.TryProduceProduct((OvenData)Data);        
        }

        private void OnIngredientsFormRecipe(PCBaseEventData baseEventData)
        {
            var eventData = (OvenIngredientsFormedRecipe)baseEventData;
            if (eventData.Oven.Id == Data.Id)
            {
                _image.color = Color.red;
            }
        }

        protected override void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnOvenIngredientsFormRecipe, OnIngredientsFormRecipe);
        }

        protected override void UnRegisterEventListeners() 
        {
            Manager.EventManager.RemoveListener(PCEventType.OnOvenIngredientsFormRecipe, OnIngredientsFormRecipe);
        }
    }
}
