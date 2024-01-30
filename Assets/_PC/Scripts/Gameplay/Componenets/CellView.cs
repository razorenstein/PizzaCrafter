using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Gameplay.Componenets;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class CellView : PCMonoBehaviour, IDropHandler
    {
        public CellData Data;
        [SerializeField]
        private Image _item;

        public void Initialize(CellData data)
        {
            Data = data;
            Debug.Log($"Initialized cell at {Data.Position.Row}, {Data.Position.Column}");
        }

        public void OnDrop(PointerEventData eventData)
        {
             Manager.BoardManager.TryMoveTile(eventData.pointerDrag.GetComponent<TileView>().Data, Data.Position, out var movementType);   
        }
    }
}
