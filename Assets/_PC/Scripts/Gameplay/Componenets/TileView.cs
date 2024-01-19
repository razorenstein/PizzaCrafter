using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
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
        }

        public void OnDragDrop(CellData cellData)
        {
            BoardView.Instance.OnTileDragDrop(cellData);
        }

        private void LoadSprite(string addressableKey)
        {
            AddressablesHelper.TryLoadAddressable(addressableKey,
                loadedSprite =>
                {
                    _image.sprite = loadedSprite;
                    Debug.Log("Sprite loaded successfully.");
                },
                errorMessage =>
                {
                    Debug.LogError(errorMessage);
                });
        }
    }
}
