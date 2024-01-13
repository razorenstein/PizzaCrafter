using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Resources;
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
        [SerializeField]
        private TMP_Text _buttonText;

        public void Initialize(ResourceData data)
        {
            _data = data;
            _button.onClick.AddListener(OnClick);
            _buttonText.text = _data.Name;
        }

        public void OnClick() 
        { 
            Manager.ResourceManager.TryGetResourceLoot(_data.Type);
        }
    }
}
