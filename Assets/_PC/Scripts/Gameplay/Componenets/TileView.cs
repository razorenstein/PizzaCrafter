using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class TileView : PCMonoBehaviour
    {
        private TileData _data;
        [SerializeField]
        private Image _image;

        public void Initialize(TileData data)
        {
            _data = data;
            LoadSprite(data.Ingredient.SpriteAddressableKey);
            Debug.Log($"Initialized tile at {_data.Position.Row}, {_data.Position.Column}");
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
