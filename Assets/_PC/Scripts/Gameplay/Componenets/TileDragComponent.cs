using Assets._PC.Scripts.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Assets._PC.Scripts.Gameplay.Componenets.Helpers
{
    public class TileDragComponent : PCMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private TileView _itemToDrag;
        [SerializeField]
        private CanvasGroup _canvasGroup;
        private Vector3 _startPosition;
        private Transform _startParent;
        private Transform _dragParent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _itemToDrag.BoardView.SetDraggedTile(_itemToDrag);
            _startParent = _itemToDrag.transform.parent;
            //in order to render it above all cells for the drag
            _dragParent = _itemToDrag.BoardView.transform;
            _canvasGroup.blocksRaycasts = false;
            _startPosition = _itemToDrag.transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.SetParent(_dragParent);
            _itemToDrag.RectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            if (_itemToDrag.transform.parent == _dragParent)
            {
                _itemToDrag.transform.position = _startPosition;
                _itemToDrag.transform.SetParent(_startParent);
            }

            _itemToDrag.BoardView.RemoveDraggedTile();
        }
    }
}
