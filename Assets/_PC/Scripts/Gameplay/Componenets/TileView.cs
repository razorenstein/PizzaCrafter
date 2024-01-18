using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class TileView : PCMonoBehaviour
    {
        public TileData Data;
        public BoardView BoardView;
        [SerializeField]
        public RectTransform RectTransform;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private CanvasGroup _canvasGroup;

        public void Initialize(TileData data, BoardView board)
        {
            Data = data;
            BoardView = board;
            LoadSprite(data.Ingredient.SpriteAddressableKey);
        }

        public void OnDragDrop(CellData cellData)
        {
            BoardView.OnTileDragDrop(cellData);
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
