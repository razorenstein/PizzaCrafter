using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public abstract class TileView : PCMonoBehaviour
    {
        public TileData Data;
        [SerializeField]
        public RectTransform RectTransform;
        [SerializeField]
        protected Image _image;
        [SerializeField]
        protected CanvasGroup _canvasGroup;

        public virtual void Initialize(TileData data)
        {
            Data = data;
            LoadSprite(data.SpriteAddressableKey);
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnDragDrop(CellData cellData)
        {
            BoardView.Instance.OnTileDragDrop(cellData);
        }
        public void Unload()
        {
            _image.sprite = null;
        }

        private async void LoadSprite(string addressableKey)
        {
            try
            {
                Sprite loadedSprite = await AddressablesHelper.TryLoadAddressableAsync(addressableKey);
                _image.sprite = loadedSprite;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}
