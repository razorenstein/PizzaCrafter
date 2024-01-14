using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Resources;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class ResourceView : PCMonoBehaviour
    {
        private ResourceData _data;
        [SerializeField]
        private Button _button;
        //[SerializeField]
        //private TMP_Text _buttonText;
        [SerializeField]
        private Image _image;

        public void Initialize(ResourceData data)
        {
            _data = data;
            _button.onClick.AddListener(OnClick);
            //_buttonText.text = _data.Name;
            LoadSprite(_data.SpriteAddressableKey);
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

        private void OnClick() 
        { 
            Manager.ResourceManager.TryGetResourceLoot(_data.Type);
        }
    }
}
