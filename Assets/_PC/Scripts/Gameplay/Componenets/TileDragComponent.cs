using Assets._PC.Scripts.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using Assets._PC.Scripts.Core.Managers;

namespace Assets._PC.Scripts.Gameplay.Componenets.Helpers
{
    public class TileDragComponent : PCMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private TileView _itemToDrag;
        [SerializeField]
        private CanvasGroup _canvasGroup;
        private BoardView _boardView;
        private Vector3 _startPosition;
        private Transform _startParent;
        private Transform _dragParent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_itemToDrag.IsDraggable)
                return;
            _boardView = BoardView.Instance;
            _boardView.SetDraggedTile(_itemToDrag);
            _startParent = _itemToDrag.transform.parent;
            //in order to render it above all cells for the drag
            _dragParent = _boardView.transform;
            _canvasGroup.blocksRaycasts = false;
            _startPosition = _itemToDrag.transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_itemToDrag.IsDraggable)
                return;

            transform.SetParent(_dragParent);
            _itemToDrag.RectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_itemToDrag.IsDraggable)
                return;

            _canvasGroup.blocksRaycasts = true;
            if (_itemToDrag.transform.parent == _dragParent)
            {
                _itemToDrag.transform.position = _startPosition;
                _itemToDrag.transform.SetParent(_startParent);
            }

            _boardView.RemoveDraggedTile();
        }
    }
}
