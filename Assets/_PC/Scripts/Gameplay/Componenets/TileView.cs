using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using CodiceApp.EventTracking;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public abstract class TileView : PCMonoBehaviour
    {
        public TileData Data;
        public bool IsDraggable { get; private set; } = true;
        [SerializeField]
        public RectTransform RectTransform;
        [SerializeField]
        protected Image _image;
        [SerializeField]
        protected CanvasGroup _canvasGroup;

        public async virtual Task Initialize(TileData data)
        {
            Data = data;
            await LoadSprite(data.SpriteAddressableKey);
            _canvasGroup.blocksRaycasts = true;
            RegisterEventListeners();
        }

        public void SetDraggable()
        {
            _image.color = Color.white;
            IsDraggable = true;
        }

        public void SetUnDraggable()
        {
            //in order to make it clickable to remove from oven
            _canvasGroup.blocksRaycasts = true;
            _image.color = Color.grey;
            IsDraggable = false;
        }

        public virtual void Deinitialize()
        {
            _image.sprite = null;
            UnRegisterEventListeners();
        }

        protected virtual void RegisterEventListeners() {}
        protected virtual void UnRegisterEventListeners() {}

        private async Task LoadSprite(string addressableKey)
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
