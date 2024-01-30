using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Resources;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class ResourceTileView : TileView, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {      
            var resourcedata = (ResourceData) Data;
            Manager.ResourceManager.TryLootResource(resourcedata.ResourceType);
        }
    }
}
